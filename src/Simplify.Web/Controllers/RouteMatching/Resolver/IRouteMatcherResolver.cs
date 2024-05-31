using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers.RouteMatching.Resolver;

/// <summary>
/// Represents a route matcher resolver.
/// </summary>
public interface IRouteMatcherResolver
{
	/// <summary>
	/// Resolves a route matcher by controller metadata.
	/// </summary>
	/// <param name="controller">The controller.</param>
	IRouteMatcher Resolve(IControllerMetadata controller);
}