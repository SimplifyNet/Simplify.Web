using System;
using Owin;
using Simplify.Web.Bootstrapper;

namespace Simplify.Web.Owin;

/// <summary>
/// OWIN IAppBuilder Simplify.Web extensions
/// </summary>
public static class AppBuilderExtensions
{
	/// <summary>
	/// Adds Simplify.Web to OWIN pipeline
	/// </summary>
	/// <param name="builder">The OWIN builder.</param>
	/// <returns></returns>
	public static IAppBuilder UseSimplifyWeb(this IAppBuilder builder)
	{
		try
		{
			BootstrapperFactory.CreateBootstrapper().Register();
			return builder.Use<SimplifyWebOwinMiddleware>();
		}
		catch (Exception e)
		{
			SimplifyWebOwinMiddleware.ProcessOnException(e);

			throw;
		}
	}
}