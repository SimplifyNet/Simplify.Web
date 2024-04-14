using System.Collections.Generic;
using Simplify.Web.Old.Meta2;

namespace Simplify.Web.Old.Core2.Controllers.RouteMatching;

public class MatchedController(IControllerMetaData metaData, IDictionary<string, object>? routeParameters = null) : IMatchedController
{
	public IControllerMetaData MetaData { get; } = metaData;

	public IDictionary<string, object>? RouteParameters { get; } = routeParameters;
}