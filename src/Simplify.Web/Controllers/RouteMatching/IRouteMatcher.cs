using System.Collections.Generic;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Meta.Routing;

namespace Simplify.Web.Controllers.RouteMatching;

/// <summary>
/// Represent an HTTP route parser and matcher.
/// </summary>
public interface IRouteMatcher
{
	/// <summary>
	/// Determines whether this route matcher can handle the specified controller.
	/// </summary>
	/// <param name="controller">The controller.</param>
	/// <returns>
	///   <c>true</c> if this route matcher can handle the specified controller; otherwise, <c>false</c>.
	/// </returns>
	bool CanHandle(IControllerMetadata controller);

	/// <summary>
	/// Matches the specified route.
	/// </summary>
	/// <param name="currentPath">The current path.</param>
	/// <param name="controllerRoute">The controller route.</param>
	IRouteMatchResult Match(IList<string> currentPath, IControllerRoute controllerRoute);
}