namespace Simplify.Web.Controllers.Security;

/// <summary>
/// Provides the controller security status.
/// </summary>
public enum SecurityStatus
{
	/// <summary>
	/// OK status.
	/// </summary>
	Ok,

	/// <summary>
	/// The user is not authenticated.
	/// </summary>
	Unauthorized,

	/// <summary>
	/// The user is authenticated but does not have access rights.
	/// </summary>
	Forbidden
}