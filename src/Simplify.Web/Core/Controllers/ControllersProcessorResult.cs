namespace Simplify.Web.Core.Controllers;

/// <summary>
/// Controllers processor result types
/// </summary>
public enum ControllersProcessorResult
{
	/// <summary>
	/// OK
	/// </summary>
	Ok,

	/// <summary>
	/// Execution should be stopped, because raw output will be sent to client
	/// </summary>
	RawOutput,

	/// <summary>
	/// The HTTP 400 error should be sent to response
	/// </summary>
	Http400,

	/// <summary>
	/// The HTTP 401 error should be sent to response
	/// </summary>
	Http401,

	/// <summary>
	/// The HTTP 403 error should be sent to response
	/// </summary>
	Http403,

	/// <summary>
	/// The HTTP 404 error should be sent to response
	/// </summary>
	Http404,

	/// <summary>
	/// Execution should be stopped, because client will be redirected to new URL
	/// </summary>
	Redirect
}