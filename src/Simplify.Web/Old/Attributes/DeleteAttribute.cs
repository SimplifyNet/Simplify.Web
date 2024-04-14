using System;

namespace Simplify.Web.Old.Attributes;

/// <summary>
/// Set controller HTTP DELETE request route path.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DeleteAttribute"/> class.
/// </remarks>
/// <param name="route">The route.</param>
[AttributeUsage(AttributeTargets.Class)]
public class DeleteAttribute(string route) : ControllerRouteAttribute(route)
{
}