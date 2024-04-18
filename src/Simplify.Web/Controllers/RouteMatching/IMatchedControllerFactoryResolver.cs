using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.RouteMatching;

public interface IMatchedControllerFactoryResolver
{
	IMatchedControllerFactory Resolve(IControllerMetadata metaData);
}