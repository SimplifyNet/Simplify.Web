namespace Simplify.Web.Controllers.Security;

/// <summary>
/// Provides security rules check result.
/// </summary>
public enum SecurityStatus
{
	/// <summary>
	/// OK result.
	/// </summary>
	Ok,

	/// <summary>
	/// The user is not authenticated.
	/// </summary>
	NotAuthenticated,

	/// <summary>
	/// The user is authenticated but does not have access rights.
	/// </summary>
	Forbidden
}