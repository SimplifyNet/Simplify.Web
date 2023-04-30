#nullable disable

namespace Simplify.Web.Routing;

/// <summary>
/// Provides HTTP route matching result
/// </summary>
public class RouteMatchResult : IRouteMatchResult
{
	/// <summary>
	/// Initializes a new instance of the <see cref="RouteMatchResult" /> class.
	/// </summary>
	/// <param name="matched">if set to <c>true</c> then it means what matchind was successfull.</param>
	/// <param name="routeParameters">The route parameters.</param>
	public RouteMatchResult(bool matched = false, dynamic routeParameters = null)
	{
		Success = matched;
		RouteParameters = routeParameters;
	}

	/// <summary>
	/// Gets a value indicating whether the route was matched successfully
	/// </summary>
	/// <value>
	/// <c>true</c> if the route was matched successfully; otherwise, <c>false</c>.
	/// </value>
	public bool Success { get; }

	/// <summary>
	/// Gets the route parsed parameters.
	/// </summary>
	/// <value>
	/// The route parsed parameters.
	/// </value>
	public dynamic RouteParameters { get; }
}