using System.Collections.Generic;

namespace Simplify.Web.Controllers.RouteMatching;

/// <summary>
/// Represent an HTTP route matching result.
/// </summary>
public interface IRouteMatchResult
{
	/// <summary>
	/// Gets the value indicating whether the route was matched.
	/// </summary>
	/// <value>
	///   <c>true</c> if success; otherwise, <c>false</c>.
	/// </value>
	bool Success { get; }

	/// <summary>
	/// Gets the route parsed parameters.
	/// </summary>
	/// <value>
	/// The route parameters.
	/// </value>
	IReadOnlyDictionary<string, object> RouteParameters { get; }
}