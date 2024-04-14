using Simplify.Web.Core2.Controllers.RouteMatching;
using Simplify.Web.Core2.Http;

namespace Simplify.Web.Core2.Controllers.Processing.Context;

public interface IControllerProcessingContextFactory
{
	IControllerProcessingContext Create(IMatchedController controller, IHttpContext context);
}