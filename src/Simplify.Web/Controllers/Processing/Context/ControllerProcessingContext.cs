using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.RouteMatching;
using Simplify.Web.Controllers.Security;

namespace Simplify.Web.Controllers.Processing.Context;

public class ControllerProcessingContext(
	IMatchedController matchedController,
	SecurityStatus securityStatus,
	HttpContext context) : IControllerProcessingContext
{
	public IMatchedController Controller { get; } = matchedController;
	public SecurityStatus SecurityStatus { get; } = securityStatus;
	public HttpContext HttpContext { get; } = context;
}