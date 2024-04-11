using Simplify.Web.Meta2;

namespace Simplify.Web.Core2.Controllers.Routing;

public interface IMatchedControllerFactoryResolver
{
	IMatchedControllerFactory Resolve(IControllerMetaData metaData);
}
