using System.Collections.Generic;
using Simplify.Web.Meta2;

namespace Simplify.Web.Core2.Controllers.Routing;

public class MatchedController(IControllerMetaData metaData, IDictionary<string, object>? routeParameters = null) : IMatchedController
{
	public IControllerMetaData MetaData { get; } = metaData;

	public IDictionary<string, object>? RouteParameters { get; } = routeParameters;
}