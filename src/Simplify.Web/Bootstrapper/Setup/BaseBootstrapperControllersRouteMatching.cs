﻿using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Controllers.RouteMatching;
using Simplify.Web.Controllers.RouteMatching.Resolver;
using Simplify.Web.Controllers.V1.Routing;
using Simplify.Web.Controllers.V2.Routing;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper controllers route matching registrations.
/// </summary>
public partial class BaseBootstrapper
{
	/// <summary>
	/// Registers the route matcher resolver.
	/// </summary>
	public virtual void RegisterRouteMatcherResolver()
	{
		if (TypesToExclude.Contains(typeof(IRouteMatcherResolver)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IRouteMatcherResolver, RouteMatcherResolver>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the route matcher resolver matchers.
	/// </summary>
	public virtual void RegisterRouteMatcherResolverMatchers()
	{
		if (TypesToExclude.Contains(typeof(IReadOnlyList<IRouteMatcher>)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IReadOnlyList<IRouteMatcher>>(r =>
			[
				new Controller2RouteMatcher(),
				new Controller1RouteMatcher()
			], LifetimeType.Singleton);
	}
}