using Simplify.DI;
using Simplify.Web.Old.Core.AccessorsBuilding;
using Simplify.Web.Old.Modules.Data;

namespace Simplify.Web.Old.Core.Controllers.Execution;

/// <summary>
/// Provides v2 controller factory.
/// </summary>
public class Controller2Factory : ActionModulesAccessorBuilder
{
	/// <summary>
	/// Creates the controller.
	/// </summary>
	/// <param name="args">The controller execution args.</param>
	/// <returns>The controller.</returns>
	public ResponseShortcutsControllerBase CreateController(IControllerExecutionArgs args)
	{
		var controller = (Controller2Base)args.Resolver.Resolve(args.ControllerMetaData.ControllerType);

		BuildActionModulesAccessorProperties(controller, args.Resolver);

		controller.StringTable = args.Resolver.Resolve<IStringTable>().Items;

		return controller;
	}
}