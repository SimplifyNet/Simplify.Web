using System;
using Simplify.Web.Core.AccessorsBuilding;

namespace Simplify.Web.Core.Controllers.Execution;

/// <summary>
/// Provides v2 controller factory
/// </summary>
public class Controller2Factory : ActionModulesAccessorBuilder, IController2Factory
{
	/// <summary>
	/// Creates the controller.
	/// </summary>
	/// <param name="args">The controller execution args.</param>
	/// <returns>The controller.</returns>
	public ResponseShortcutsControllerBase CreateController(IControllerExecutionArgs args)
	{
		throw new NotImplementedException();
	}
}