using System.Collections.Generic;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Meta.Routing;
using Simplify.Web.Controllers.RouteMatching;
using Simplify.Web.Controllers.V1.Metadata;

namespace Simplify.Web.Controllers.V1.Routing;

/// <summary>
/// Provides the controller v1 route matcher.
/// </summary>
/// <seealso cref="IRouteMatcher" />
public class Controller1RouteMatcher : IRouteMatcher
{
	/// <summary>
	/// Determines whether this route matcher can handle the specified controller.
	/// </summary>
	/// <param name="controller">The controller.</param>
	/// <returns>
	///   <c>true</c> if this route matcher can handle the specified controller; otherwise, <c>false</c>.
	/// </returns>
	public bool CanHandle(IControllerMetadata controller) => controller is IController1Metadata;

	/// <summary>
	/// Matches the specified route.
	/// </summary>
	/// <param name="currentPath">The current path.</param>
	/// <param name="controllerRoute">The controller route.</param>
	public IRouteMatchResult Match(IList<string> currentPath, IControllerRoute controllerRoute) =>
		StringConverterBasedRouteMatcher.Match(currentPath, controllerRoute);
}