using System.Collections.Generic;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Controllers;

public class MatchedController(IControllerMetadata metaData, IDictionary<string, object>? routeParameters = null) : IMatchedController
{
	public IControllerMetadata Controller { get; } = metaData;

	public IDictionary<string, object>? RouteParameters { get; } = routeParameters;
}