using System.Dynamic;
using Simplify.DI;
using Simplify.Web.Modules.Data;
using Simplify.Web.PropertiesInjection;
using Simplify.Web.System;

namespace Simplify.Web.Controllers.V1.Execution;

/// <summary>
/// Provides the controller v1 factory.
/// </summary>
/// <seealso cref="ActionModulesAccessorInjector" />
/// <seealso cref="IController1Factory" />
public class Controller1Factory(IDIResolver resolver) : ActionModulesAccessorInjector(resolver), IController1Factory
{
	private readonly IDIResolver _resolver = resolver;

	/// <summary>
	/// Creates the controller.
	/// </summary>
	/// <param name="matchedController">The matched controller</param>
	/// <returns>
	/// The controller.
	/// </returns>
	public ControllerBase CreateController(IMatchedController matchedController)
	{
		var controller = (ControllerBase)_resolver.Resolve(matchedController.Controller.ControllerType);

		InjectActionModulesAccessorProperties(controller);

		controller.RouteParameters = matchedController.RouteParameters?.ToExpandoObject() ?? new ExpandoObject()!;
		controller.StringTable = _resolver.Resolve<IStringTable>().Items;

		return controller;
	}
}