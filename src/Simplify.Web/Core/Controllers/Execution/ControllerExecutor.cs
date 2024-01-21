using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.DI;
using Simplify.Web.Meta;

namespace Simplify.Web.Core.Controllers.Execution;

/// <summary>
/// Provides controller executor, handles creation and execution of controllers
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ControllerExecutor"/> class.
/// </remarks>
/// <param name="controllerFactory">The controller factory.</param>
/// <param name="controllerResponseBuilder">The controller response builder.</param>
public class ControllerExecutor(IController1Factory controllerFactory, IControllerResponseBuilder controllerResponseBuilder) : IControllerExecutor
{
	private readonly IController1Factory _controllerFactory = controllerFactory;
	private readonly IControllerResponseBuilder _controllerResponseBuilder = controllerResponseBuilder;

	/// <summary>
	/// Creates and executes the specified controller.
	/// </summary>
	/// <param name="controllerMetaData">Type of the controller.</param>
	/// <param name="resolver">The DI container resolver.</param>
	/// <param name="context">The context.</param>
	/// <param name="routeParameters">The route parameters.</param>
	/// <returns></returns>
	public async Task<ControllerResponseResult> Execute(IControllerMetaData controllerMetaData, IDIResolver resolver, HttpContext context,
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

		if (response == null)
			return ControllerResponseResult.Default;

		_controllerResponseBuilder.BuildControllerResponseProperties(response, resolver);

		return await response.Process();
	}
}