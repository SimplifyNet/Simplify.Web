using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.RouteMatching.Resolver;

public class RouteMatcherResolver(IReadOnlyList<IRouteMatcher> matchers) : IRouteMatcherResolver
{
	public IRouteMatcher Resolve(IControllerMetadata controller) =>
		matchers.FirstOrDefault(x => x.CanHandle(controller))
		?? throw new InvalidOperationException("No matching route matcher found for controller type: " + controller.ControllerType);
}