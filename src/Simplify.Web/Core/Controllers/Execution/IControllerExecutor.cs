using System;
using System.Collections.Generic;
using Microsoft.Owin;
using Simplify.DI;

#nullable disable

namespace Simplify.Web.Core.Controllers.Execution
{
	/// <summary>
	/// Represent controller executor, handles creation and execution of controllers
	/// </summary>
	public interface IControllerExecutor
	{
		/// <summary>
		/// Creates and executes the specified controller.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <param name="resolver">The DI container resolver.</param>
		/// <param name="context">The context.</param>
		/// <param name="routeParameters">The route parameters.</param>
		/// <returns></returns>
		ControllerResponseResult Execute(Type controllerType, IDIResolver resolver, IOwinContext context,
			dynamic routeParameters = null);

		/// <summary>
		/// Processes the asynchronous controllers responses.
		/// </summary>
		/// <param name="resolver">The DI container resolver.</param>
		/// <returns></returns>
		IEnumerable<ControllerResponseResult> ProcessAsyncControllersResponses(IDIResolver resolver);
	}
}