using System.Collections.Generic;
using System.Security.Claims;
using Simplify.Web.Meta;

namespace Simplify.Web.Core2.Controllers.Security;

public class SecurityChecker(IList<ISecurityRule> checks) : ISecurityChecker
{
	public SecurityStatus CheckSecurityRules(IControllerMetaData metaData, ClaimsPrincipal? user)
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