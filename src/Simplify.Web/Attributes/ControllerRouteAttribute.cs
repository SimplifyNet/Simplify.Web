using System;

namespace Simplify.Web.Attributes;

/// <summary>
/// Set controller request route path.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="GetAttribute"/> class.
/// </remarks>
/// <param name="route">The route.</param>
[AttributeUsage(AttributeTargets.Class)]
public class ControllerRouteAttribute(string route) : Attribute
{

	/// <summary>
	/// Gets the route.
	/// </summary>
	/// <value>
	/// The route.
	/// </value>
	public string Route { get; } = route;
}