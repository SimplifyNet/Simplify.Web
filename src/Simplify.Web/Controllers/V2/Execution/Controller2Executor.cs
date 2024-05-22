using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Simplify.Web.Controllers.Execution;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.V2.Metadata;

namespace Simplify.Web.Controllers.V2.Execution;

/// <summary>
///  Provides v2 controller executor.
/// </summary>
/// <param name="controllerFactory">The v2 controller factory.</param>
public class Controller2Executor(IController2Factory controllerFactory) : IControllerExecutor
{
	public bool CanHandle(IControllerMetadata controllerMetadata) => controllerMetadata is IController2Metadata;

	public Task<ControllerResponse?> ExecuteAsync(IMatchedController matchedController)
	{
		var controllerMetadata = (IController2Metadata)matchedController.Controller;
		var controller = controllerFactory.CreateController(matchedController);
		var methodParams = ConstructMethodParams(controllerMetadata.InvokeMethodParameters, matchedController.RouteParameters!);

		return InvokeAsync(controllerMetadata.InvokeMethodInfo, methodParams, controller);
	}

	private static async Task<ControllerResponse?> InvokeAsync(MethodInfo methodInfo, IList<object> methodParams,
		ResponseShortcutsControllerBase controller)
	{
		var result = methodInfo.Invoke(controller, [.. methodParams]);

		switch (result)
		{
			case ControllerResponse response:
				return response;

			case Task<ControllerResponse> response:
				await response;
				return response.Result;

			case Task task:
				await task;
				break;
		}

		return null;
	}

	private static IList<object> ConstructMethodParams(IDictionary<string, Type> invokeMethodParameters, IReadOnlyDictionary<string, object> routeParameters)
	{
		var result = new List<object>(invokeMethodParameters.Count);

		foreach (var item in invokeMethodParameters.Keys)
		{
			if (!routeParameters.TryGetValue(item, out var parameter))
				throw new InvalidOperationException($"Route parameter with the name '{item}' is not found.");

			result.Add(parameter);
		}

		return result;
	}
}