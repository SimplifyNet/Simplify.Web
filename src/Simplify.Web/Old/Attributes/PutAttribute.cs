using System;

namespace Simplify.Web.Old.Attributes;

/// <summary>
/// Set controller HTTP PUT request route path.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PutAttribute"/> class.
/// </remarks>
/// <param name="route">The route.</param>
[AttributeUsage(AttributeTargets.Class)]
public class PutAttribute(string route) : ControllerRouteAttribute(route)
{
}