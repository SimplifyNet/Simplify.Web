using System.Security.Claims;
using Simplify.Web.Old.Meta2;

namespace Simplify.Web.Old.Core2.Controllers.Security.Rules;

public class AuthorizationRule : ISecurityRule
{
	public SecurityStatus Check(ControllerSecurity security, ClaimsPrincipal? user) =>
		security is not { IsAuthorizationRequired: true }
			? SecurityStatus.Ok
			: SecurityStatus.Forbidden;
}