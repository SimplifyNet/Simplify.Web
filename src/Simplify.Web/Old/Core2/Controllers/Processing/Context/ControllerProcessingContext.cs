using Simplify.Web.Old.Core2.Controllers.RouteMatching;
using Simplify.Web.Old.Core2.Controllers.Security;
using Simplify.Web.Old.Http;

namespace Simplify.Web.Old.Core2.Controllers.Processing.Context;

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