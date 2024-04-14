using Simplify.Web.Old.Core2.Controllers.RouteMatching;
using Simplify.Web.Old.Http;

namespace Simplify.Web.Old.Core2.Controllers.Processing.Context;

public interface IControllerProcessingContextFactory
{
	IControllerProcessingContext Create(IMatchedController controller, IHttpContext context);
}