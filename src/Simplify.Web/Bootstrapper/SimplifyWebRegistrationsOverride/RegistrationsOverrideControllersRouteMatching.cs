using System;
using System.Collections.Generic;
using Simplify.DI;
using Simplify.Web.Controllers.RouteMatching;
using Simplify.Web.Controllers.RouteMatching.Resolver;

namespace Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

/// <summary>
/// Provides the Simplify.Web controllers route matching override.
/// </summary>
public partial class RegistrationsOverride
{
	/// <summary>
	/// Overrides the `IRouteMatcherResolver` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideRouteMatcherResolver(Action<IDIRegistrator> action) => AddAction<IRouteMatcherResolver>(action);

	/// <summary>
	/// Overrides the `IReadOnlyList&lt;IRouteMatcher&gt;` registration.
	/// </summary>
	/// <param name="action">The custom registration action.</param>
	public RegistrationsOverride OverrideRouteMatcherResolverMatchers(Action<IDIRegistrator> action) => AddAction<IReadOnlyList<IRouteMatcher>>(action);
}