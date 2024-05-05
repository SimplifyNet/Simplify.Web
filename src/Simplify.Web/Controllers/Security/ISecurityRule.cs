using System.Security.Claims;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.Security;

public interface ISecurityRule
{
	SecurityStatus ViolationStatus { get; }

	bool IsViolated(ControllerSecurity security, ClaimsPrincipal? user);
}