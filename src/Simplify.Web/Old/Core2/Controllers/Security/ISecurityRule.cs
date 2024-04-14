using System.Security.Claims;
using Simplify.Web.Old.Meta2;

namespace Simplify.Web.Old.Core2.Controllers.Security;

public interface ISecurityRule
{
	SecurityStatus Check(ControllerSecurity security, ClaimsPrincipal? user);
}