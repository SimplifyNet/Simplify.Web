using System.Security.Claims;
using Simplify.Web.Meta;

namespace Simplify.Web.Core2.Controllers.Security;

public interface ISecurityChecker
{
	/// <summary>
	/// Determines whether controller security rules violated.
	/// </summary>
	/// <param name="metaData">The controller metadata.</param>
	/// <param name="user">The current request user.</param>
	/// <returns></returns>
	SecurityStatus CheckSecurityRules(IControllerMetaData metaData, ClaimsPrincipal? user);
}