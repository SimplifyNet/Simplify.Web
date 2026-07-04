using System;

namespace Simplify.Web.Attributes;

/// <summary>
/// Sets the controller HTTP QUERY request route path.
/// </summary>
/// <seealso cref="ControllerRouteAttribute" />
/// <remarks>
/// Initializes a new instance of the <see cref="QueryAttribute" /> class.
/// </remarks>
/// <param name="route">The route.</param>
[AttributeUsage(AttributeTargets.Class)]
public class QueryAttribute(string route) : ControllerRouteAttribute(route);
