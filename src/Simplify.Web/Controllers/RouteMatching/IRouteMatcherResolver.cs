using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.RouteMatching;

public interface IRouteMatcherResolver
{
	IRouteMatcher Resolve(IControllerMetadata controller);
}