using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.Security;

public class SecurityChecker(IReadOnlyList<ISecurityRule> checks) : ISecurityChecker
{
	public SecurityStatus CheckSecurityRules(IControllerMetadata metaData, ClaimsPrincipal? user)
	{
		if (metaData.Security is not { IsAuthorizationRequired: true })
			return SecurityStatus.Ok;

		return (from check in checks
				where check.IsViolated(metaData.Security, user)
				select check.ViolationStatus)
			.FirstOrDefault();
	}
}