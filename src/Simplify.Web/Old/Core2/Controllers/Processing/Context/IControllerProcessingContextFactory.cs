using Simplify.Web.Http;
using Simplify.Web.Old.Core2.Controllers.RouteMatching;

namespace Simplify.Web.Old.Core2.Controllers.Processing.Context;

public interface IControllerProcessingContextFactory
{
	IControllerProcessingContext Create(IMatchedController controller, IHttpContext context);
}