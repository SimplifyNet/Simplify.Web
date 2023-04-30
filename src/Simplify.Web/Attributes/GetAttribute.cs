using System;

namespace Simplify.Web.Attributes;

/// <summary>
/// Set controller HTTP GET request route path
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class GetAttribute : Attribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="GetAttribute"/> class.
	/// </summary>
	/// <param name="route">The route.</param>
	public GetAttribute(string route)
	{
		Route = route;
	}

	/// <summary>
	/// Gets the route.
	/// </summary>
	/// <value>
	/// The route.
	/// </value>
	public string Route { get; private set; }
}