using System;

namespace Simplify.Web.Attributes;

/// <summary>
/// Sets the controller HTTP GET request route path.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="GetAttribute"/> class.
/// </remarks>
/// <param name="route">The route.</param>
[AttributeUsage(AttributeTargets.Class)]
public class GetAttribute(string route) : ControllerRouteAttribute(route);