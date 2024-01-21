using Simplify.Web.Core.AccessorsBuilding;

namespace Simplify.Web.Core.Controllers.Execution;

/// <summary>
/// Provides v1 controller factory
/// </summary>
public class Controller1Factory : ActionModulesAccessorBuilder, IController1Factory
{
	/// <summary>
	/// Creates the controller.
	/// </summary>
	/// <param name="args">The controller execution args.</param>
	/// <returns>The controller.</returns>
	public ControllerBase CreateController(IControllerExecutionArgs args)
	{
		var controller = (ControllerBase)args.Resolver.Resolve(args.ControllerMetaData.ControllerType);

		BuildActionModulesAccessorProperties(controller, args.Resolver);

		controller.RouteParameters = args.RouteParameters;

		return controller;
	}
}