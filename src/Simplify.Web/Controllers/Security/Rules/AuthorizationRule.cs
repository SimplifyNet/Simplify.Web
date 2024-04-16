using System.Security.Claims;
using Simplify.Web.Meta;

namespace Simplify.Web.Controllers.Security.Rules;

public class AuthorizationRule : ISecurityRule
{
	public SecurityStatus Check(ControllerSecurity security, ClaimsPrincipal? user) =>
		security is not { IsAuthorizationRequired: true }
			? SecurityStatus.Ok
			: SecurityStatus.Forbidden;
}