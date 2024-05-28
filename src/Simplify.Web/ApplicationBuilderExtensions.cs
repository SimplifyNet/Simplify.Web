using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Simplify.Web.Bootstrapper;
using Simplify.Web.Middleware;

namespace Simplify.Web;

/// <summary>
/// Provides the IApplicationBuilder Simplify.Web extensions.
/// </summary>
public static class ApplicationBuilderExtensions
{
	/// <summary>
	/// Adds Simplify.Web to ASP.NET Core pipeline as a terminal middleware and optionally performs Simplify.Web bootstrapper registrations.
	/// </summary>
	/// <param name="builder">The application builder.</param>
	/// <param name="autoRegisterSimplifyWebTypes">Determines whether SimplifyWeb types should be registered in IOC container automatically.</param>
	public static IApplicationBuilder UseSimplifyWeb(this IApplicationBuilder builder, bool autoRegisterSimplifyWebTypes = false)
	{
		try
		{
			if (autoRegisterSimplifyWebTypes)
				BootstrapperFactory.CreateBootstrapper().Register();

			builder.RegisterAsTerminal();

			return builder;
		}
		catch (Exception e)
		{
			SimplifyWebRequestMiddleware.ProcessOnException(e);

			throw;
		}
	}

	/// <summary>
	/// Adds Simplify.Web to ASP.NET Core pipeline as a non-terminal middleware and optionally performs Simplify.Web bootstrapper registrations.
	/// </summary>
	/// <param name="builder">The application builder.</param>
	/// <param name="autoRegisterSimplifyWebTypes">Determines whether SimplifyWeb types should be registered in IOC container automatically.</param>
	public static IApplicationBuilder UseSimplifyWebNonTerminal(this IApplicationBuilder builder, bool autoRegisterSimplifyWebTypes = false)
	{
		try
		{
			if (autoRegisterSimplifyWebTypes)
				BootstrapperFactory.CreateBootstrapper().Register();

			builder.RegisterAsNonTerminal();

			return builder;
		}
		catch (Exception e)
		{
			SimplifyWebRequestMiddleware.ProcessOnException(e);

			throw;
		}
	}

	private static void RegisterAsNonTerminal(this IApplicationBuilder builder) =>
		builder.Use(async (context, next) =>
		{
			await SimplifyWebRequestMiddleware.InvokeAsNonTerminalAsync(context);

			if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
				await next.Invoke();
		});

	private static void RegisterAsTerminal(this IApplicationBuilder builder) => builder.Run(SimplifyWebRequestMiddleware.InvokeAsTerminalAsync);
}