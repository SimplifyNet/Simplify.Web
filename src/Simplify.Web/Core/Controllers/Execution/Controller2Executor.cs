using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.DI;
using Simplify.Web.Meta;

namespace Simplify.Web.Core.Controllers.Execution;

/// <summary>
///  Provides v1 controllers executor
/// </summary>
/// <param name="controllerFactory">The controller factory.</param>
public class Controller2Executor(IController2Factory controllerFactory) : IVersionedControllerExecutor
{
	private readonly IController2Factory _controllerFactory = controllerFactory;

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
	public Task<ControllerResponse?> Execute(IControllerMetaData controllerMetaData, IDIResolver resolver, HttpContext context,
		IDictionary<string, object>? routeParameters = null)
	{
		throw new NotImplementedException();
	}
}
