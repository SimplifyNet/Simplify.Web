using System;

namespace Simplify.Web.Attributes;

/// <summary>
/// Set controller HTTP PUT request route path
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class PutAttribute : Attribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="PutAttribute"/> class.
	/// </summary>
	/// <param name="route">The route.</param>
	public PutAttribute(string route)
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