using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Simplify.DI;

namespace Simplify.Web.Core.Controllers.Execution;

/// <summary>
/// Represent controller factory
/// </summary>
public interface IController1Factory
{
	/// <summary>
	/// Creates the controller.
	/// </summary>
	/// <param name="controllerType">Type of the controller.</param>
	/// <param name="resolver">The DI container resolver.</param>
	/// <param name="context">The context.</param>
	/// <param name="routeParameters">The route parameters.</param>
	/// <returns></returns>
	ControllerBase CreateController(Type controllerType, IDIResolver resolver, HttpContext context, IDictionary<string, object>? routeParameters = null);
}