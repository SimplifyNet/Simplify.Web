#nullable disable

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.DI;
using Simplify.Web.Bootstrapper;
using Simplify.Web.Core;
using Simplify.Web.Diagnostics;
using Simplify.Web.Diagnostics.Measurement;
using Simplify.Web.Diagnostics.Trace;
using Simplify.Web.Modules;
using Simplify.Web.Settings;

namespace Simplify.Web.RequestPipeline
{
	/// <summary>
	/// Simplify.Web request execution root
	/// </summary>
	public class SimplifyWebRequestMiddleware
	{
		/// <summary>
		/// Occurs when exception occurred and catched by framework.
		/// </summary>
		public static event ExceptionEventHandler OnException;

		/// <summary>
		/// Occurs on each request.
		/// </summary>
		public static event TraceEventHandler OnTrace;

		/// <summary>
		/// Process an individual request.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static async Task<RequestHandlingStatus> Invoke(HttpContext context)
		{
			using var scope = BootstrapperFactory.ContainerProvider.BeginLifetimeScope();

			try
			{
				return await scope.StartMeasurements()
					.Trace(context, OnTrace)
					.SetupProviders(context)
					.ProcessRequest(context);
			}
			catch (Exception e)
			{
				try
				{
					context.Response.StatusCode = 500;

					ProcessOnException(e);
				}
				catch (Exception exception)
				{
					e = exception;
				}

				await context.Response.WriteAsync(GenerateErrorPage(scope, e));

				return RequestHandlingStatus.RequestWasHandled;
			}
		}

		internal static bool ProcessOnException(Exception e)
		{
			if (OnException == null)
				return false;

			OnException(e);
			return true;
		}

		private static string GenerateErrorPage(ILifetimeScope scope, Exception e)
		{
			var settings = scope.Resolver.Resolve<ISimplifyWebSettings>();

			return ErrorPageGenerator.Generate(e, settings.HideExceptionDetails, settings.ErrorPageDarkStyle);
		}
	}
}