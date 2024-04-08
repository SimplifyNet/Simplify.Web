using System.Security.Claims;
using Simplify.Web.Meta;

namespace Simplify.Web.Core2.Controllers.Processing.Security.Rules;

public class AuthenticationRule : ISecurityRule
{
	public SecurityStatus Check(ControllerSecurity security, ClaimsPrincipal? user) =>
		user?.Identity is { IsAuthenticated: true }
			? SecurityStatus.Ok
			: SecurityStatus.NotAuthenticated;
}