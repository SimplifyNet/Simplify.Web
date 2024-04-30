using System.Collections.Generic;

namespace Simplify.Web.Controllers.RouteMatching.Matcher;

/// <summary>
/// Represent an HTTP route matching result.
/// </summary>
public interface IRouteMatchResult
{
	/// <summary>
	/// Gets the value indicating whether the route was matched.
	/// </summary>
	bool Success { get; }

	/// <summary>
	/// Gets the route parsed parameters.
	/// </summary>
	IReadOnlyDictionary<string, object>? RouteParameters { get; }
}