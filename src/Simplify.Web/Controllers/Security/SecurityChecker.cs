using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.Security;

/// <summary>
/// Provides the security checker.
/// </summary>
/// <seealso cref="ISecurityChecker" />
public class SecurityChecker(IReadOnlyList<ISecurityRule> checks) : ISecurityChecker
{
	/// <summary>
	/// Determines whether controller security rules violated.
	/// </summary>
	/// <param name="metaData">The controller metadata.</param>
	/// <param name="user">The current request user.</param>
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