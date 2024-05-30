using System;

namespace Simplify.Web.Attributes;

/// <summary>
/// Set the controller HTTP POST request route path.
/// </summary>
/// <seealso cref="ControllerRouteAttribute" />
/// <remarks>
/// Initializes a new instance of the <see cref="PostAttribute" /> class.
/// </remarks>
/// <param name="route">The route.</param>
[AttributeUsage(AttributeTargets.Class)]
public class PostAttribute(string route) : ControllerRouteAttribute(route);