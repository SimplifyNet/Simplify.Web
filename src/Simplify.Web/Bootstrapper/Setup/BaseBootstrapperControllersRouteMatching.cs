using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Controllers.RouteMatching;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper controllers route matching registrations.
/// </summary>
public partial class BaseBootstrapper
{
	public virtual void RegisterRouteMatcherResolver()
	{
		if (TypesToExclude.Contains(typeof(IRouteMatcherResolver)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IRouteMatcherResolver, RouteMatcherResolver>(LifetimeType.Singleton);
	}

	public virtual void RegisterRouteMatcherResolverMatchers()
	{
		if (TypesToExclude.Contains(typeof(IReadOnlyList<IRouteMatcher>)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IReadOnlyList<IRouteMatcher>>(r =>
			new List<IRouteMatcher>
			{
			}, LifetimeType.Singleton);
	}
}