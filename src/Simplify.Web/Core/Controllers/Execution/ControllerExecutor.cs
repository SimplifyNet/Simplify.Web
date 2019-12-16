using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.DI;
using Simplify.Web.Core.Controllers.Execution.Building;
using Simplify.Web.Meta;

namespace Simplify.Web.Core.Controllers.Execution
{
	/// <summary>
	/// Provides controller executor, handles creation and execution of controllers
	/// </summary>
	public class ControllerExecutor : IControllerExecutor
	{
		private readonly IControllerFactory _controllerFactory;
		private readonly IControllerResponseBuilder _controllerResponseBuilder;

		private readonly IList<Task<ControllerResponse>> _controllersResponses = new List<Task<ControllerResponse>>();

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerExecutor"/> class.
		/// </summary>
		/// <param name="controllerFactory">The controller factory.</param>
		/// <param name="controllerResponseBuilder">The controller response builder.</param>
		public ControllerExecutor(IControllerFactory controllerFactory, IControllerResponseBuilder controllerResponseBuilder)
		{
			_controllerFactory = controllerFactory;
			_controllerResponseBuilder = controllerResponseBuilder;
		}

		/// <summary>
		/// Creates and executes the specified controller.
		/// </summary>
		/// <param name="controllerMetaData">Type of the controller.</param>
		/// <param name="resolver">The DI container resolver.</param>
		/// <param name="context">The context.</param>
		/// <param name="routeParameters">The route parameters.</param>
		/// <returns></returns>
		public async Task<ControllerResponseResult> Execute(IControllerMetaData controllerMetaData, IDIResolver resolver, HttpContext context,
			dynamic? routeParameters = null)
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

			return await response.ProcessAsync();
		}
	}
}