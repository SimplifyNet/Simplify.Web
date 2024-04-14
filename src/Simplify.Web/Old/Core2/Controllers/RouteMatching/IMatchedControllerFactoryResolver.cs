using Simplify.Web.Old.Meta2;

namespace Simplify.Web.Old.Core2.Controllers.RouteMatching;

public interface IMatchedControllerFactoryResolver
{
	IMatchedControllerFactory Resolve(IControllerMetaData metaData);
}
