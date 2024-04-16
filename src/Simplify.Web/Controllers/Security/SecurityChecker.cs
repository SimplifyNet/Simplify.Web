using System.Collections.Generic;
using System.Security.Claims;
using Simplify.Web.Meta;

namespace Simplify.Web.Controllers.Security;

public class SecurityChecker(IReadOnlyList<ISecurityRule> checks) : ISecurityChecker
{
	public SecurityStatus CheckSecurityRules(IControllerMetadata metaData, ClaimsPrincipal? user)
	{
		if (metaData.Security is not { IsAuthorizationRequired: true })
			return SecurityStatus.Ok;

		foreach (var check in checks)
		{
			var result = check.Check(metaData.Security, user);

			if (result != SecurityStatus.Ok)
				return result;
		}

		return SecurityStatus.Ok;
	}
}