using System.Security.Claims;

namespace Simplify.Web.Core2.Controllers.Security;

public interface ISecurityRule
{
	SecurityStatus Check(ControllerSecurity security, ClaimsPrincipal? user);
}