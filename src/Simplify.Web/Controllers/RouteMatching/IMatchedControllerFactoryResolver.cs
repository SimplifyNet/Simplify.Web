using Simplify.Web.Meta;

namespace Simplify.Web.Controllers.RouteMatching;

public interface IMatchedControllerFactoryResolver
{
	IMatchedControllerFactory Resolve(IControllerMetadata metaData);
}