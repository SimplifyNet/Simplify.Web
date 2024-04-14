using Simplify.Web.Meta2;

namespace Simplify.Web.Core2.Controllers.RouteMatching;

public interface IMatchedControllerFactory
{
	bool CanCreate(IControllerMetaData metaData);

	IMatchedController Create(IControllerMetaData metaData, IHttpContext context);
}
