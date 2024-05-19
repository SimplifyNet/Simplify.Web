using System;
using System.Collections.Generic;

namespace Simplify.Web.Controllers.RouteMatching;

/// <summary>
/// Provides the HTTP route matching result.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="RouteMatchResult" /> class.
/// </remarks>
public class RouteMatchResult : IRouteMatchResult
{
	/// <summary>
	/// Provides the HTTP route matching result.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="RouteMatchResult" /> class.
	/// </remarks>
	/// <param name="matched">if set to <c>true</c> then it means what matching was successful.</param>
	public RouteMatchResult(bool matched = false)
	{
		Success = matched;
		RouteParameters = new Dictionary<string, object>();
	}

	/// <summary>
	/// Provides the HTTP route matching result.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the <see cref="RouteMatchResult" /> class.
	/// </remarks>
	/// <param name="matched">if set to <c>true</c> then it means what matching was successful.</param>
	/// <param name="routeParameters">The route parameters.</param>
	public RouteMatchResult(bool matched, IReadOnlyDictionary<string, object> routeParameters)
	{
		Success = matched;
		RouteParameters = routeParameters ?? throw new ArgumentNullException(nameof(routeParameters));
	}

	/// <summary>
	/// Gets a value indicating whether the route was matched successfully.
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
	public IReadOnlyDictionary<string, object> RouteParameters { get; }
}