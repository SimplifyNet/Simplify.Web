using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.DI;
using Simplify.Web.Meta;

namespace Simplify.Web.Core.Controllers.Execution.V1;

/// <summary>
///  Provides v1 controllers executor
/// </summary>
/// <param name="controllerFactory">The controller factory.</param>
public class ControllerExecutor1(IControllerFactory controllerFactory) : IVersionedControllerExecutor
{
	private readonly IControllerFactory _controllerFactory = controllerFactory;

	/// <summary>
	/// Gets the controller version
	/// </summary>
	public short Version => 1;

	/// <summary>
	/// Creates and executes the specified controller.
	/// </summary>
	/// <param name="controllerMetaData">Type of the controller.</param>
	/// <param name="resolver">The DI container resolver.</param>
	/// <param name="context">The context.</param>
	/// <param name="routeParameters">The route parameters.</param>
	/// <returns></returns>
	public async Task<ControllerResponse?> Execute(IControllerMetaData controllerMetaData, IDIResolver resolver, HttpContext context,
		IDictionary<string, object>? routeParameters = null)
	{
		ControllerResponse? response = null;
		var controller = _controllerFactory.CreateController(controllerMetaData.ControllerType, resolver, context, routeParameters);

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