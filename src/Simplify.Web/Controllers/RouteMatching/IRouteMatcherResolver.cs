using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.RouteMatching;

public interface IRouteMatcherResolver
{
	IRouteMatcher Resolve(IControllerMetadata controller);
}