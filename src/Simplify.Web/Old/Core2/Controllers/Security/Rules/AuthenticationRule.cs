using System.Security.Claims;
using Simplify.Web.Old.Meta2;

namespace Simplify.Web.Old.Core2.Controllers.Security.Rules;

public class AuthenticationRule : ISecurityRule
{
	public SecurityStatus Check(ControllerSecurity security, ClaimsPrincipal? user) =>
		user?.Identity is { IsAuthenticated: true }
			? SecurityStatus.Ok
			: SecurityStatus.NotAuthenticated;
}