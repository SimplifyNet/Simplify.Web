using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Simplify.Web.Old.Bootstrapper;
using Simplify.Web.Old.Middleware;

namespace Simplify.Web.Old;

/// <summary>
/// IApplicationBuilder Simplify.Web extensions.
/// </summary>
public static class ApplicationBuilderExtensions
{
	/// <summary>
	/// Performs Simplify.Web bootstrapper registrations and adds Simplify.Web to the OWIN pipeline as a terminal middleware.
	/// </summary>
	/// <param name="builder">The OWIN builder.</param>
	public static IApplicationBuilder UseSimplifyWeb(this IApplicationBuilder builder)
	{
		try
		{
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
	/// Adds Simplify.Web to the OWIN pipeline as a terminal middleware without bootstrapper registrations (useful when you want to control bootstrapper registrations manually).
	/// </summary>
	/// <param name="builder">The builder.</param>
	public static IApplicationBuilder UseSimplifyWebWithoutRegistrations(this IApplicationBuilder builder)
	{
		try
		{
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
	/// Performs Simplify.Web bootstrapper registrations and adds Simplify.Web to the OWIN pipeline as a non-terminal middleware.
	/// </summary>
	/// <param name="builder">The OWIN builder.</param>
	public static IApplicationBuilder UseSimplifyWebNonTerminal(this IApplicationBuilder builder)
	{
		try
		{
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

	/// <summary>
	/// Adds Simplify.Web to the OWIN pipeline as a non-terminal middleware without bootstrapper registrations.
	/// </summary>
	/// <param name="builder">The OWIN builder.</param>
	public static IApplicationBuilder UseSimplifyWebNonTerminalWithoutRegistrations(this IApplicationBuilder builder)
	{
		try
		{
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