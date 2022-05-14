namespace Simplify.Web.Core;

/// <summary>
/// Provides request handling status
/// </summary>
public enum RequestHandlingStatus
{
	/// <summary>
	/// The request was unhandled
	/// </summary>
	RequestWasUnhandled = 0,

	/// <summary>
	/// The request was handled
	/// </summary>
	RequestWasHandled = 1,
}