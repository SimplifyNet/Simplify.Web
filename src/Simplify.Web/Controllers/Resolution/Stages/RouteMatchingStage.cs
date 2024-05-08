using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Resolution.State;
using Simplify.Web.Controllers.RouteMatching.Resolver;
using Simplify.Web.Http;

namespace Simplify.Web.Controllers.Resolution.Stages;

public class RouteMatchingStage(IRouteMatcherResolver routeMatcherResolver) : IControllerResolutionStage
{
	public void Execute(ControllerResolutionState state, HttpContext context, Action stopExecution)
	{
		var execParameters = state.Controller.ExecParameters
			?? throw new InvalidOperationException($"Controller execution parameters should not be null, controller type: '{state.Controller.GetType().Name}'");

		var item = execParameters.Routes.FirstOrDefault(x => x.Key == Converter.HttpMethodStringToToHttpMethod(context.Request.Method));

		if (default(KeyValuePair<HttpMethod, string>).Equals(item))
		{
			stopExecution();
			return;
		}

		var result = routeMatcherResolver.Resolve(state.Controller).Match(context.Request.Path.Value, item.Value);

		state.IsMatched = result.Success;

		if (result.Success)
			state.RouteParameters = result.RouteParameters;
		else
			stopExecution();
	}
}