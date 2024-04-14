using System;

namespace Simplify.Web.Attributes;

/// <summary>
/// Set controller HTTP OPTIONS request route path.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="OptionsAttribute"/> class.
/// </remarks>
/// <param name="route">The route.</param>
[AttributeUsage(AttributeTargets.Class)]
public class OptionsAttribute(string route) : ControllerRouteAttribute(route)
{
}