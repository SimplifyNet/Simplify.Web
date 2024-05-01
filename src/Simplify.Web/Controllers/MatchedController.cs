using System.Collections.Generic;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers;

public class MatchedController(IControllerMetadata metaData, IReadOnlyDictionary<string, object>? routeParameters = null) : IMatchedController
{
	public IControllerMetadata Controller { get; } = metaData;

	public IReadOnlyDictionary<string, object>? RouteParameters { get; } = routeParameters;
}