using System.Security.Claims;
using Simplify.Web.Meta;

namespace Simplify.Web.Controllers.Security;

public interface ISecurityRule
{
	SecurityStatus Check(ControllerSecurity security, ClaimsPrincipal? user);
}