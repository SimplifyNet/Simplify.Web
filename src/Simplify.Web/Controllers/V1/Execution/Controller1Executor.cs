using System;
using System.Threading.Tasks;
using Simplify.Web.Controllers.Execution;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.V1.Metadata;

namespace Simplify.Web.Controllers.V1.Execution;

/// <summary>
/// Provides the v1 controller executor.
/// </summary>
/// <seealso cref="IControllerExecutor" />
/// <param name="controllerFactory">The v1 controller factory.</param>
public class Controller1Executor(IController1Factory controllerFactory) : IControllerExecutor
{
	/// <summary>
	/// Determines whether this executor can execute the controller.
	/// </summary>
	/// <param name="controllerMetadata">The controller metadata.</param>
	public bool CanHandle(IControllerMetadata controllerMetadata) => controllerMetadata is IController1Metadata;

	/// <summary>
	/// Creates an actual controller and executes it.
	/// </summary>
	/// <param name="matchedController">The matched controller.</param>
	public async Task<ControllerResponse?> ExecuteAsync(IMatchedController matchedController)
	{
		var controller = controllerFactory.CreateController(matchedController);

		return controller switch
		{
			SyncControllerBase syncController => syncController.Invoke(),
			AsyncControllerBase asyncController => await asyncController.Invoke(),
			_ => throw new InvalidOperationException($"Incorrect controller base class, controller type: {matchedController.Controller.GetType().Name}"),
		};
	}
}