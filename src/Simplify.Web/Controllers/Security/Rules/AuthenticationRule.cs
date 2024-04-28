using System.Security.Claims;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.Security.Rules;

public class AuthenticationRule : ISecurityRule
{
	public SecurityStatus ViolationStatus => SecurityStatus.NotAuthenticated;

	public bool IsViolated(ControllerSecurity security, ClaimsPrincipal? user) => user?.Identity == null || !user.Identity.IsAuthenticated;
}