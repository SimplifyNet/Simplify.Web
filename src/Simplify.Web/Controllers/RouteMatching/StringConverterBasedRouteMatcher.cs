using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Controllers.Meta.Routing;
using Simplify.Web.System;

namespace Simplify.Web.Controllers.RouteMatching;

public static class StringConverterBasedRouteMatcher
{
	public static IRouteMatchResult Match(IList<string> currentPath, IControllerRoute controllerRoute)
	{
		// Run on all pages route
		if (controllerRoute.Items.Count == 0 && controllerRoute.Path == "")
			return new RouteMatchResult(true);

		if (currentPath.Count != controllerRoute.Items.Count)
			return new RouteMatchResult();

		var routeParameters = new Dictionary<string, object>();

		return TryMatchPathItems(currentPath, controllerRoute.Items, routeParameters)
			? new RouteMatchResult(true, routeParameters)
			: new RouteMatchResult();
	}

	private static bool TryMatchPathItems(IList<string> currentPath, IList<PathItem> controllerRouteItems, Dictionary<string, object> routeParameters) =>
		!controllerRouteItems.Where((currentItem, i) => !MatchPathItem(currentItem, currentPath[i], routeParameters)).Any();

	private static bool MatchPathItem(PathItem item, string currentPathSegment, Dictionary<string, object> routeParameters)
	{
		switch (item)
		{
			case PathSegment segment:
				return segment.Name == currentPathSegment;

			case PathParameter parameter:
				var value = StringConverter.TryConvert(parameter.Type, currentPathSegment);

				if (value == null)
					return false;

				routeParameters.Add(parameter.Name, value);
				return true;

			default:
				return false;
		}
	}
}