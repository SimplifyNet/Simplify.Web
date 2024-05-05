using System.Security.Claims;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.Security;

public interface ISecurityChecker
{
	/// <summary>
	/// Determines whether controller security rules violated.
	/// </summary>
	/// <param name="metaData">The controller metadata.</param>
	/// <param name="user">The current request user.</param>
	SecurityStatus CheckSecurityRules(IControllerMetadata metaData, ClaimsPrincipal? user);
}