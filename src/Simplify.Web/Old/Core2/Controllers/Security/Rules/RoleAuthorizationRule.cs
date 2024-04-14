using System.Linq;
using System.Security.Claims;
using Simplify.Web.Old.Meta2;

namespace Simplify.Web.Old.Core2.Controllers.Security.Rules;

public class RoleAuthorizationRule : ISecurityRule
{
	public SecurityStatus Check(ControllerSecurity security, ClaimsPrincipal? user)
	{
		if (security.RequiredUserRoles == null || !security.RequiredUserRoles.Any())
			return SecurityStatus.Ok;

		if (user != null && security.RequiredUserRoles.Any(user.IsInRole))
			return SecurityStatus.Ok;

		return SecurityStatus.Forbidden;
	}
}