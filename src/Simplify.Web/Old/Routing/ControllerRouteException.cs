using System;

namespace Simplify.Web.Routing;

/// <summary>
/// Provides controller route exception.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ControllerRouteException" /> class.
/// </remarks>
/// <param name="message">The message that describes the error.</param>
[Serializable]
public class ControllerRouteException(string message) : Exception(message)
{
}