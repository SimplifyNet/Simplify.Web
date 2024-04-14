namespace Simplify.Web.Old;

/// <summary>
/// Provides controller response result types.
/// </summary>
public enum ControllerResponseResult
{
	/// <summary>
	/// The default result (no special processing required).
	/// </summary>
	Default,

	/// <summary>
	/// The execution should be stopped, because raw output will be sent to the client.
	/// </summary>
	RawOutput,

	/// <summary>
	/// The execution should be stopped, because the client will be redirected to the new URL.
	/// </summary>
	Redirect
}