using Simplify.Web.Core2.Controllers.Processing.Security;
using Simplify.Web.Core2.Controllers.Routing;
using Simplify.Web.Core2.Http;

namespace Simplify.Web.Core2.Controllers.Processing.Context;

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
