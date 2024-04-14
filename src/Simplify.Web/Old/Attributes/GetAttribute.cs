using System;

namespace Simplify.Web.Old.Attributes;

/// <summary>
/// Set controller HTTP GET request route path.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="GetAttribute"/> class.
/// </remarks>
/// <param name="route">The route.</param>
[AttributeUsage(AttributeTargets.Class)]
public class GetAttribute(string route) : ControllerRouteAttribute(route)
{
}