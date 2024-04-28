using System.Collections.Generic;
using System.Security.Claims;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.Security;

public class SecurityChecker(IReadOnlyList<ISecurityRule> checks) : ISecurityChecker
{
	public SecurityStatus CheckSecurityRules(IControllerMetadata metaData, ClaimsPrincipal? user)
	{
		if (metaData.Security is not { IsAuthorizationRequired: true })
			return SecurityStatus.Ok;

		foreach (var check in checks)
			if (check.IsViolated(metaData.Security, user))
				return check.ViolationStatus;

		return SecurityStatus.Ok;
	}
}