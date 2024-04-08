using System.Security.Claims;
using Simplify.Web.Meta;

namespace Simplify.Web.Core2.Controllers.Processing.Security;

public interface ISecurityRule
{
	SecurityStatus Check(ControllerSecurity security, ClaimsPrincipal? user);
}