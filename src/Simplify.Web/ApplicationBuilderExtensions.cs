using System;
using Microsoft.AspNetCore.Builder;
using Simplify.Web.Bootstrapper;
using Simplify.Web.Core;
using Simplify.Web.RequestPipeline;

namespace Simplify.Web
{
	/// <summary>
	/// IApplicationBuilder Simplify.Web extensions
	/// </summary>
	public static class ApplicationBuilderExtensions
	{
		/// <summary>
		/// Gets or sets a value indicating whether Simplify.Web is terminal middleware.
		/// </summary>
		public static bool TerminalMiddleware { get; set; } = true;

		/// <summary>
		/// Performs Simplify.Web bootstrapper registrations and adds Simplify.Web to the OWIN pipeline as a terminal middleware
		/// </summary>
		/// <param name="builder">The OWIN builder.</param>
		/// <returns></returns>
		public static IApplicationBuilder UseSimplifyWeb(this IApplicationBuilder builder)
		{
			try
			{
				TerminalMiddleware = true;
				BootstrapperFactory.CreateBootstrapper().Register();

				RegisterAsTerminal(builder);

				return builder;
			}
			catch (Exception e)
			{
				SimplifyWebRequestMiddleware.ProcessOnException(e);

				throw;
			}
		}

		/// <summary>
		/// Adds Simplify.Web to the OWIN pipeline as a terminal middleware without bootstrapper registrations (useful when you want to control bootstrapper registrations manually)
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <returns></returns>
		public static IApplicationBuilder UseSimplifyWebWithoutRegistrations(this IApplicationBuilder builder)
		{
			try
			{
				TerminalMiddleware = true;

				RegisterAsTerminal(builder);

				return builder;
			}
			catch (Exception e)
			{
				SimplifyWebRequestMiddleware.ProcessOnException(e);

				throw;
			}
		}

		/// <summary>
		/// Performs Simplify.Web bootstrapper registrations and adds Simplify.Web to the OWIN pipeline as a non-terminal middleware
		/// </summary>
		/// <param name="builder">The OWIN builder.</param>
		/// <returns></returns>
		public static IApplicationBuilder UseSimplifyWebNonTerminal(this IApplicationBuilder builder)
		{
			try
			{
				TerminalMiddleware = false;
				BootstrapperFactory.CreateBootstrapper().Register();

				Register(builder);

				return builder;
			}
			catch (Exception e)
			{
				SimplifyWebRequestMiddleware.ProcessOnException(e);

				throw;
			}
		}

		/// <summary>
		/// Adds Simplify.Web to the OWIN pipeline as a non-terminal middleware without bootstrapper registrations
		/// </summary>
		/// <param name="builder">The OWIN builder.</param>
		/// <returns></returns>
		public static IApplicationBuilder UseSimplifyWebNonTerminalWithoutRegistrations(this IApplicationBuilder builder)
		{
			try
			{
				TerminalMiddleware = false;

				Register(builder);

				return builder;
			}
			catch (Exception e)
			{
				SimplifyWebRequestMiddleware.ProcessOnException(e);

				throw;
			}
		}

		private static void Register(IApplicationBuilder builder)
		{
			builder.Use(async (context, next) =>
			{
				var result = SimplifyWebRequestMiddleware.Invoke(context);

				await result.Task;

				if (result.Status == RequestHandlingStatus.RequestWasUnhandled)
					await next.Invoke();
			});
		}

		private static void RegisterAsTerminal(IApplicationBuilder builder)
		{
			builder.Run(async (context) => await SimplifyWebRequestMiddleware.Invoke(context).Task);
		}
	}
}