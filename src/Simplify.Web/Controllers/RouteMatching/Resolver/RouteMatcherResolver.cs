using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.RouteMatching.Resolver;

/// <summary>
/// Provides the route matcher resolver.
/// </summary>
/// <seealso cref="IRouteMatcherResolver" />
public class RouteMatcherResolver(IReadOnlyList<IRouteMatcher> matchers) : IRouteMatcherResolver
{
	/// <summary>
	/// Resolves a route matcher by controller metadata.
	/// </summary>
	/// <param name="controller">The controller.</param>
	/// <returns></returns>
	/// <exception cref="InvalidOperationException">No matching route matcher found for controller type: " + controller.ControllerType</exception>
	public IRouteMatcher Resolve(IControllerMetadata controller) =>
		matchers.FirstOrDefault(x => x.CanHandle(controller))
		?? throw new InvalidOperationException("No matching route matcher found for controller type: " + controller.ControllerType);
}