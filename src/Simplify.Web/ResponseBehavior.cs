namespace Simplify.Web;

/// <summary>
/// Provides the response behavior types.
/// </summary>
public enum ResponseBehavior
{
	/// <summary>
	/// The default behavior (no special processing required).
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