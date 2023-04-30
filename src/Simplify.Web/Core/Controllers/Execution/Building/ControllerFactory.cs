using System;
using Microsoft.Owin;
using Simplify.DI;
using Simplify.Web.Core.AccessorsBuilding;

#nullable disable

namespace Simplify.Web.Core.Controllers.Execution.Building;

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
	public ControllerBase CreateController(Type controllerType, IDIResolver resolver, IOwinContext context,
		dynamic routeParameters = null)
	{
		var controller = (ControllerBase)resolver.Resolve(controllerType);

		BuildActionModulesAccessorProperties(controller, resolver);

		controller.RouteParameters = routeParameters;

		return controller;
	}
}