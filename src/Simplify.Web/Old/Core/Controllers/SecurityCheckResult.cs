namespace Simplify.Web.Old.Core.Controllers;

/// <summary>
/// Security rules check result.
/// </summary>
public enum SecurityRuleCheckResult
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