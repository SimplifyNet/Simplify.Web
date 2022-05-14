using System;

namespace Simplify.Web.Attributes;

/// <summary>
/// Set controller HTTP GET request route path
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class GetAttribute : ControllerRouteAttribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="GetAttribute"/> class.
	/// </summary>
	/// <param name="route">The route.</param>
	public GetAttribute(string route) : base(route)
	{
	}
}