using Simplify.Web.Old.Http;
using Simplify.Web.Old.Meta2;

namespace Simplify.Web.Old.Core2.Controllers.RouteMatching;

public interface IMatchedControllerFactory
{
	bool CanCreate(IControllerMetaData metaData);

	IMatchedController Create(IControllerMetaData metaData, IHttpContext context);
}