using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.RouteMatching;

/// <summary>
/// Represent a HTTP route parser and matcher.
/// </summary>
public interface IRouteMatcher
{
	bool CanHandle(IControllerMetadata controller);

	/// <summary>
	/// Matches the specified route.
	/// </summary>
	/// <param name="currentPath">The current path.</param>
	/// <param name="controllerPath">The controller path.</param>
	IRouteMatchResult Match(string? currentPath, string? controllerPath);
}