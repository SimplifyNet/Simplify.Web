using System;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.RouteMatching;

namespace Simplify.Web.Controllers.Resolution.Stages;

public class RouteMatchingStage(IRouteMatcherResolver routeMatcherResolver) : IControllerResolutionStage
{
	public void Execute(ControllerResolutionState state, HttpContext context, Action stopExecution)
	{
		var result = routeMatcherResolver.Resolve(state.ControllerMetadata).Match(null, null);

		state.IsMatched = result.Success;

		if (result.Success)
			state.RouteParameters = result.RouteParameters;
		else
			stopExecution();
	}
}