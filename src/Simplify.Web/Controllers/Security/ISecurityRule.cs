using System.Security.Claims;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.Security;

public interface ISecurityRule
{
	SecurityStatus ViolationStatus { get; }

	bool IsViolated(ControllerSecurity security, ClaimsPrincipal? user);
}