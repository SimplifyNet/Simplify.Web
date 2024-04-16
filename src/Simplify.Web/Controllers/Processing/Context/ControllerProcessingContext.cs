using Simplify.Web.Controllers.RouteMatching;
using Simplify.Web.Controllers.Security;
using Simplify.Web.Http;

namespace Simplify.Web.Controllers.Processing.Context;

public class ControllerProcessingContext(
	IMatchedController matchedController,
	SecurityStatus securityStatus,
	IHttpContext context) : IControllerProcessingContext
{
	public IMatchedController Controller { get; } = matchedController;
	public SecurityStatus SecurityStatus { get; } = securityStatus;
	public IHttpContext Context { get; } = context;

	public void SetResponseStatusCode(int code) => Context.Context.Response.StatusCode = code;
}