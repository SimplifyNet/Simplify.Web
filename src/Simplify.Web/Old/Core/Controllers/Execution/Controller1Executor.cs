using System.Threading.Tasks;
using Simplify.Web.Old.Meta;

namespace Simplify.Web.Old.Core.Controllers.Execution;

/// <summary>
///  Provides v1 controller executor.
/// </summary>
/// <param name="controllerFactory">The v1 controller factory.</param>
public class Controller1Executor(IController1Factory controllerFactory) : IVersionedControllerExecutor
{
	private readonly IController1Factory _controllerFactory = controllerFactory;

	/// <summary>
	/// Gets the controller version.
	/// </summary>
	public ControllerVersion Version => ControllerVersion.V1;

	/// <summary>
	/// Creates and executes the controller.
	/// </summary>
	/// <param name="args">The controller execution args.</param>
	/// <returns>The controller response.</returns>
	public async Task<ControllerResponse?> Execute(IControllerExecutionArgs args)
	{
		ControllerResponse? response = null;
		var controller = _controllerFactory.CreateController(args);

		switch (controller)
		{
			case SyncControllerBase syncController:
				{
					response = syncController.Invoke();
					break;
				}

			case AsyncControllerBase asyncController:
				{
					response = await asyncController.Invoke();
					break;
				}
		}

		return response;
	}
}