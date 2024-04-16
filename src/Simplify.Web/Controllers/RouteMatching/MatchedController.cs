using System.Collections.Generic;
using Simplify.Web.Meta;

namespace Simplify.Web.Controllers.RouteMatching;

public class MatchedController(IControllerMetadata metaData, IDictionary<string, object>? routeParameters = null) : IMatchedController
{
	public IControllerMetadata MetaData { get; } = metaData;

	public IDictionary<string, object>? RouteParameters { get; } = routeParameters;
}