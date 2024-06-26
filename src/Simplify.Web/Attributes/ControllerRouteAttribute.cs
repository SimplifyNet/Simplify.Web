﻿using System;

namespace Simplify.Web.Attributes;

/// <summary>
/// Sets the controller request route path.
/// </summary>
/// <seealso cref="Attribute" />
/// <remarks>
/// Initializes a new instance of the <see cref="GetAttribute" /> class.
/// </remarks>
/// <param name="route">The route.</param>
[AttributeUsage(AttributeTargets.Class)]
public class ControllerRouteAttribute(string route) : Attribute
{
	/// <summary>
	/// Gets the route.
	/// </summary>
	public string Route { get; } = route;
}