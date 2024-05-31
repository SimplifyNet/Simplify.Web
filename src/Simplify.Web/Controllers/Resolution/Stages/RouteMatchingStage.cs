using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Meta.Routing;
using Simplify.Web.Controllers.Resolution.State;
using Simplify.Web.Controllers.RouteMatching.Resolver;
using Simplify.Web.Http;
using Simplify.Web.Http.RequestPath;

namespace Simplify.Web.Controllers.Resolution.Stages;

/// <summary>
/// Provides the route matching stage.
/// </summary>
/// <seealso cref="IControllerResolutionStage" />
public class RouteMatchingStage(IRouteMatcherResolver routeMatcherResolver) : IControllerResolutionStage
{
	/// <summary>
	/// Executes this stage.
	/// </summary>
	/// <param name="state">The state.</param>
	/// <param name="context">The context.</param>
	/// <param name="stopExecution">The action to stop execution.</param>
	/// <exception cref="InvalidOperationException">Controller execution parameters should not be null, controller type: '{state.Controller.GetType().Name}'</exception>
	public void Execute(IControllerResolutionState state, HttpContext context, Action stopExecution)
	{
		var execParameters = state.Controller.ExecParameters
			?? throw new InvalidOperationException($"Controller execution parameters should not be null, controller type: '{state.Controller.GetType().Name}'");

		var item = execParameters.Routes.FirstOrDefault(x => x.Key == Converter.HttpMethodStringToToHttpMethod(context.Request.Method));

		if (default(KeyValuePair<HttpMethod, IControllerRoute>).Equals(item))
		{
			stopExecution();
			return;
		}

		var result = routeMatcherResolver.Resolve(state.Controller).Match(context.Request.Path.Value.GetSplitPath(), item.Value);

		state.IsMatched = result.Success;

		if (result.Success)
			state.RouteParameters = result.RouteParameters;
		else
			stopExecution();
	}
}