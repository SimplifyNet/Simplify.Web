using System.Linq;
using System.Security.Claims;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.Security.Rules;

public class RoleAuthorizationRule : ISecurityRule
{
	public SecurityStatus ViolationStatus => SecurityStatus.Forbidden;

	public bool IsViolated(ControllerSecurity security, ClaimsPrincipal? user)
	{
		if (security.RequiredUserRoles == null || !security.RequiredUserRoles.Any())
			return false;

		if (user == null || !security.RequiredUserRoles.All(user.IsInRole))
			return true;

		return false;
	}
}