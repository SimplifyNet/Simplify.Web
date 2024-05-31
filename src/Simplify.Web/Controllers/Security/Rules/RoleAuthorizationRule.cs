using System.Linq;
using System.Security.Claims;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.Security.Rules;

/// <summary>
/// Provides the role authorization rule.
/// </summary>
/// <seealso cref="ISecurityRule" />
public class RoleAuthorizationRule : ISecurityRule
{
	/// <summary>
	/// Gets the violation status.
	/// </summary>
	/// <value>
	/// The violation status.
	/// </value>
	public SecurityStatus ViolationStatus => SecurityStatus.Forbidden;

	/// <summary>
	/// Determines whether this security rule is violated.
	/// </summary>
	/// <param name="security">The security.</param>
	/// <param name="user">The user.</param>
	/// <returns>
	///   <c>true</c> if this security rule is violated; otherwise, <c>false</c>.
	/// </returns>
	public bool IsViolated(ControllerSecurity security, ClaimsPrincipal? user)
	{
		if (security.RequiredUserRoles == null || !security.RequiredUserRoles.Any())
			return false;

		if (user != null && security.RequiredUserRoles.Any(user.IsInRole))
			return false;

		return true;
	}
}