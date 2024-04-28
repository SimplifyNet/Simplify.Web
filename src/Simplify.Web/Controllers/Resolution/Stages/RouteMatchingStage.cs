using System;
using Simplify.Web.Controllers.RouteMatching;
using Simplify.Web.Http;

namespace Simplify.Web.Controllers.Resolution.Stages;

public class RouteMatchingStage(IRouteMatcherResolver routeMatcherResolver) : IControllerResolutionStage
{
	public void Execute(ControllerResolutionState state, IHttpContext context, Action stopExecution)
	{
		var result = routeMatcherResolver.Resolve(state.ControllerMetadata).Match(null, null);

		state.IsMatched = result.Success;

		if (result.Success)
			state.RouteParameters = result.RouteParameters;
		else
			stopExecution();
	}
}