using System.Linq;
using Simplify.DI;
using Simplify.Web.Modules.Data;
using Simplify.Web.PropertiesInjection;

namespace Simplify.Web.Controllers.V2.Execution;

/// <summary>
/// Provides v2 controller factory.
/// </summary>
public class Controller2Factory(IDIResolver resolver) : ActionModulesAccessorInjector(resolver), IController2Factory
{
	private readonly IDIResolver _resolver = resolver;

	public ResponseShortcutsControllerBase CreateController(IMatchedController matchedController)
	{
		var controller = (Controller2Base)_resolver.Resolve(matchedController.Controller.ControllerType);

		InjectActionModulesAccessorProperties(controller);

		controller.StringTable = _resolver.Resolve<IStringTable>().Items.ToDictionary(x => x.Key, x => (string?)x.Value);

		return controller;
	}
}