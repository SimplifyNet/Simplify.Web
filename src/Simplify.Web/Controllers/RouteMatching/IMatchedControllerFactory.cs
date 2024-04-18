using Simplify.Web.Http;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.RouteMatching;

public interface IMatchedControllerFactory
{
	bool CanCreate(IControllerMetadata metaData);

	IMatchedController Create(IControllerMetadata metaData, IHttpContext context);
}