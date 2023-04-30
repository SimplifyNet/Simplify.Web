using System;

namespace Simplify.Web.Routing;

/// <summary>
/// Provides controller route exception
/// </summary>
[Serializable]
public class ControllerRouteException : Exception
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ControllerRouteException" /> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	public ControllerRouteException(string message)
		: base(message)
	{
	}
}