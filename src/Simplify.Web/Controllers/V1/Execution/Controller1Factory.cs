using System.Dynamic;
using Simplify.DI;
using Simplify.Web.Controllers.Response.Injection;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Controllers.V1.Execution;

/// <summary>
/// Provides v1 controller factory.
/// </summary>
public class Controller1Factory(IDIResolver resolver) : ActionModulesAccessorInjector(resolver), IController1Factory
{
	private readonly IDIResolver _resolver = resolver;

	/// <summary>
	/// Creates the controller.
	/// </summary>
	/// <param name="args">The controller execution args.</param>
	public ControllerBase CreateController(IMatchedController matchedController)
	{
		var controller = (ControllerBase)_resolver.Resolve(matchedController.Controller.ControllerType);

		InjectActionModulesAccessorProperties(controller);

		controller.RouteParameters = matchedController.RouteParameters ?? new ExpandoObject()!;
		controller.StringTable = _resolver.Resolve<IStringTable>().Items;

		return controller;
	}
}