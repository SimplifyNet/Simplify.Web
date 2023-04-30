namespace Simplify.Web.Routing;

/// <summary>
/// Represent HTTP route matching result
/// </summary>
public interface IRouteMatchResult
{
	/// <summary>
	/// Gets a value indicating whether the route was matched successfully
	/// </summary>
	/// <value>
	/// <c>true</c> if the route was matched successfully; otherwise, <c>false</c>.
	/// </value>
	bool Success { get; }

	/// <summary>
	/// Gets the route parsed parameters.
	/// </summary>
	/// <value>
	/// The route parsed parameters.
	/// </value>
	dynamic RouteParameters { get; }
}