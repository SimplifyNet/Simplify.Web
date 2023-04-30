using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Owin.Security.AesDataProtectorProvider;
using Simplify.DI;
using Simplify.DI.Provider.SimpleInjector;
using Simplify.Web.Meta;
using Simplify.Web.Owin;

namespace Simplify.Web.Examples.SelfHosted;

public class Startup
{
	public void Configuration(IAppBuilder app)
	{
		// Exclude Simplify.Web from exclude assemblies to be able to load example controllers
		SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Remove("Simplify");

		var provider = new SimpleInjectorDIProvider();
		DIContainer.Current = provider;

		app.UseCookieAuthentication(new CookieAuthenticationOptions
		{
			AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
			LoginPath = new PathString("/login")
		});

		app.UseAesDataProtectorProvider();

		app.UseSimplifyWeb();

		provider.Container.Verify();

		SimplifyWebOwinMiddleware.OnException += Ex;
	}

	private static void Ex(Exception e)
	{
	}
}