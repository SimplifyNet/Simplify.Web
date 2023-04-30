using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;
using Simplify.DI;
using Simplify.Web.Core.Controllers.Execution.Building;

#nullable disable

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
		/// <param name="controllerType">Type of the controller.</param>
		/// <param name="resolver">The DI container resolver.</param>
		/// <param name="context">The context.</param>
		/// <param name="routeParameters">The route parameters.</param>
		/// <returns></returns>
		public ControllerResponseResult Execute(Type controllerType, IDIResolver resolver, IOwinContext context,
			dynamic routeParameters = null)
		{
			ControllerBase controller = _controllerFactory.CreateController(controllerType, resolver, context, routeParameters);

			var syncController = controller as SyncControllerBase;

			if (syncController != null)
				return ProcessControllerResponse(syncController.Invoke(), resolver);

			var asyncController = controller as AsyncControllerBase;

			if (asyncController != null)
			{
				var task = asyncController.Invoke();
				_controllersResponses.Add(task);
			}

			return ControllerResponseResult.Default;
		}

		/// <summary>
		/// Processes the asynchronous controllers responses.
		/// </summary>
		/// <param name="resolver">The DI container resolver.</param>
		/// <returns></returns>
		public IEnumerable<ControllerResponseResult> ProcessAsyncControllersResponses(IDIResolver resolver)
		{
			foreach (var task in _controllersResponses)
			{
				task.Wait();
				yield return ProcessControllerResponse(task.Result, resolver);
			}
		}

		private ControllerResponseResult ProcessControllerResponse(ControllerResponse response, IDIResolver resolver)
		{
			if (response == null)
				return ControllerResponseResult.Default;

			_controllerResponseBuilder.BuildControllerResponseProperties(response, resolver);

			return response.Process();
		}
	}
}