using System.Collections.Generic;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Meta.Routing;
using Simplify.Web.Controllers.RouteMatching;
using Simplify.Web.Controllers.V2.Metadata;

namespace Simplify.Web.Controllers.V2.Routing;

public class Controller2RouteMatcher : IRouteMatcher
{
	public bool CanHandle(IControllerMetadata controller) => controller is IController2Metadata;

	public IRouteMatchResult Match(IList<string> currentPath, IControllerRoute controllerRoute) =>
		StringConverterBasedRouteMatcher.Match(currentPath, controllerRoute);
}