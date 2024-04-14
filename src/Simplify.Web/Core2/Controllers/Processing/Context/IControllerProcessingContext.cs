using Simplify.Web.Core2.Controllers.Processing.Security;
using Simplify.Web.Core2.Controllers.Routing;
using Simplify.Web.Core2.Http;

namespace Simplify.Web.Core2.Controllers.Processing.Context;

public interface IControllerProcessingContext
{
	public IMatchedController Controller { get; }

	public SecurityStatus SecurityStatus { get; }

	public IHttpContext Context { get; }

	void SetResponseStatusCode(int code);
}