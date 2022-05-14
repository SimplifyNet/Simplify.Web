namespace Simplify.Web;

/// <summary>
/// Controller response result types
/// </summary>
public enum ControllerResponseResult
{
	/// <summary>
	/// Default result
	/// </summary>
	Default,

	/// <summary>
	/// Execution should be stopped, because raw output will be sent to client
	/// </summary>
	RawOutput,

	/// <summary>
	/// Execution should be stopped, because client will be redirected to new URL
	/// </summary>
	Redirect
}