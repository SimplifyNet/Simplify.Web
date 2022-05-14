using System;

namespace Simplify.Web.Attributes;

/// <summary>
/// Set controller HTTP PATCH request route path
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class PatchAttribute : ControllerRouteAttribute
{
	/// <summary>
	/// Initializes a new instance of the <see cref="PatchAttribute"/> class.
	/// </summary>
	/// <param name="route">The route.</param>
	public PatchAttribute(string route) : base(route)
	{
	}
}