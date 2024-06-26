﻿using System;

namespace Simplify.Web.Attributes;

/// <summary>
/// Set the controller HTTP PUT request route path.
/// </summary>
/// <seealso cref="ControllerRouteAttribute" />
/// <remarks>
/// Initializes a new instance of the <see cref="PutAttribute" /> class.
/// </remarks>
/// <param name="route">The route.</param>
[AttributeUsage(AttributeTargets.Class)]
public class PutAttribute(string route) : ControllerRouteAttribute(route);