using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Execution;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.V1.Metadata;

namespace Simplify.Web.Controllers.V1.Execution;

/// <summary>
///  Provides v1 controller executor.
/// </summary>
/// <param name="controllerFactory">The v1 controller factory.</param>
public class Controller1Executor(IController1Factory controllerFactory) : IControllerExecutor
{
	private readonly IController1Factory _controllerFactory = controllerFactory;

	public bool CanHandle(IControllerMetadata controllerMetadata) => controllerMetadata is IController1Metadata;

	public async Task<ControllerResponse?> ExecuteAsync(IMatchedController matchedController, HttpContext context)
	{
		ControllerResponse? response = null;

		var controller = _controllerFactory.CreateController(matchedController);

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