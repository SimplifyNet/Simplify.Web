using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Simplify.DI;
using Simplify.Web.Meta;

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
		/// <param name="controllerMetaData">The controller meta-data.</param>
		/// <param name="resolver">The DI container resolver.</param>
		/// <param name="context">The context.</param>
		/// <param name="routeParameters">The route parameters.</param>
		/// <returns></returns>
		ControllerResponseResult Execute(IControllerMetaData controllerMetaData, IDIResolver resolver, HttpContext context,
			dynamic? routeParameters = null);

		/// <summary>
		/// Processes the asynchronous controllers responses.
		/// </summary>
		/// <param name="resolver">The DI container resolver.</param>
		/// <returns></returns>
		IEnumerable<ControllerResponseResult> ProcessAsyncControllersResponses(IDIResolver resolver);
	}
}