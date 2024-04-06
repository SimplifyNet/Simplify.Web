namespace Simplify.Web.Core2;

/// <summary>
/// Provides request handling status.
/// </summary>
public enum RequestHandlingStatus
{
	/// <summary>
	/// The request was unhandled.
	/// </summary>
	Unhandled = 0,

	/// <summary>
	/// The request was handled.
	/// </summary>
	Handled = 1,
}