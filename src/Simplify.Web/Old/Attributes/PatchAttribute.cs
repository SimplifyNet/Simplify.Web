using System;

namespace Simplify.Web.Old.Attributes;

/// <summary>
/// Set controller HTTP PATCH request route path.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PatchAttribute"/> class.
/// </remarks>
/// <param name="route">The route.</param>
[AttributeUsage(AttributeTargets.Class)]
public class PatchAttribute(string route) : ControllerRouteAttribute(route)
{
}