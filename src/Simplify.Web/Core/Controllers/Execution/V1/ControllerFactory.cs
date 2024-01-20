using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Simplify.DI;
using Simplify.Web.Core.AccessorsBuilding;

namespace Simplify.Web.Core.Controllers.Execution.V1;

/// <summary>
/// Controller factory
/// </summary>
public class ControllerFactory : ActionModulesAccessorBuilder, IControllerFactory
{
	/// <summary>
	/// Creates the controller.
	/// </summary>
	/// <param name="controllerType">Type of the controller.</param>
	/// <param name="resolver">The DI container resolver.</param>
	/// <param name="context">The context.</param>
	/// <param name="routeParameters">The route parameters.</param>
	/// <returns></returns>
	public ControllerBase CreateController(Type controllerType, IDIResolver resolver, HttpContext context,
		IDictionary<string, object>? routeParameters = null)
	{
		var controller = (ControllerBase)resolver.Resolve(controllerType);

		BuildActionModulesAccessorProperties(controller, resolver);

		controller.RouteParameters = routeParameters;

		return controller;
	}
}