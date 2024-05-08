using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.RouteMatching.Resolver;

public interface IRouteMatcherResolver
{
	IRouteMatcher Resolve(IControllerMetadata controller);
}