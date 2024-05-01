using System.Security.Claims;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.Security.Rules;

public class UnauthorizedRule : ISecurityRule
{
	public SecurityStatus ViolationStatus => SecurityStatus.Unauthorized;

	public bool IsViolated(ControllerSecurity security, ClaimsPrincipal? user) => user?.Identity == null || !user.Identity.IsAuthenticated;
}