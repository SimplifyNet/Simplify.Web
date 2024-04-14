using Simplify.Web.Meta2;

namespace Simplify.Web.Core2.Controllers.RouteMatching;

public interface IMatchedControllerFactoryResolver
{
	IMatchedControllerFactory Resolve(IControllerMetaData metaData);
}
