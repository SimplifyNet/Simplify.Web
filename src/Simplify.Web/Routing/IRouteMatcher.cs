namespace Simplify.Web.Routing;

/// <summary>
/// Represent HTTP route parser and matcher
/// </summary>
public interface IRouteMatcher
{
	/// <summary>
	/// Matches the specified route.
	/// </summary>
	/// <param name="currentPath">The current path.</param>
	/// <param name="controllerPath">The controller path.</param>
	/// <returns></returns>
	IRouteMatchResult Match(string currentPath, string controllerPath);
}