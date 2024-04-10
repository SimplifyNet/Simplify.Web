using System.Collections.Generic;

namespace Simplify.Web.Core2.Controllers.Routing.Matcher;

/// <summary>
/// Represent HTTP route matching result.
/// </summary>
public interface IRouteMatchResult
{
	/// <summary>
	/// Gets a value indicating whether the route was matched.
	/// </summary>
	/// <value>
	/// <c>true</c> if the route was matched; otherwise, <c>false</c>.
	/// </value>
	bool Success { get; }

	/// <summary>
	/// Gets the route parsed parameters.
	/// </summary>
	/// <value>
	/// The route parsed parameters.
	/// </value>
	IDictionary<string, object>? RouteParameters { get; }
}