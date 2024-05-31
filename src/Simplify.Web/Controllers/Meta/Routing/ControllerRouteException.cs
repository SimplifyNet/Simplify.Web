using System;

namespace Simplify.Web.Controllers.Meta.Routing;

/// <summary>
/// Provides the controller route exception.
/// </summary>
/// <seealso cref="Exception" />
/// <remarks>
/// Initializes a new instance of the <see cref="ControllerRouteException" /> class.
/// </remarks>
/// <param name="message">The message that describes the error.</param>
public class ControllerRouteException(string message) : Exception(message);