#nullable disable

using System;
using Microsoft.AspNetCore.Http;
using Simplify.DI;
using Simplify.Web.Bootstrapper;
using Simplify.Web.Core;
using Simplify.Web.Diagnostics;
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
		/// Occurs when exception occured and catched by framework.
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
		public static RequestHandlingResult Invoke(HttpContext context)
		{
			using var scope = BootstrapperFactory.ContainerProvider.BeginLifetimeScope();

			try
			{
				return scope.StartMeasurements()
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
					return RequestHandlingResult.HandledResult(
						context.Response.WriteAsync(ExceptionInfoPageGenerator.Generate(exception,
							scope.Resolver.Resolve<ISimplifyWebSettings>().HideExceptionDetails)));
				}

				return
					RequestHandlingResult.HandledResult(context.Response.WriteAsync(
						ExceptionInfoPageGenerator.Generate(e,
							scope.Resolver.Resolve<ISimplifyWebSettings>().HideExceptionDetails)));
			}
		}

		internal static bool ProcessOnException(Exception e)
		{
			if (OnException == null)
				return false;

			OnException(e);
			return true;
		}
	}
}