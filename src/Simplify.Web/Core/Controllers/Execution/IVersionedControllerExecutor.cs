﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.DI;
using Simplify.Web.Meta;

namespace Simplify.Web.Core.Controllers.Execution;

/// <summary>
/// Represent versioned controller executor, handles creation and execution of controllers
/// </summary>
public interface IVersionedControllerExecutor
{
	/// <summary>
	/// Gets the controller version
	/// </summary>
	public short Version { get; }

	/// <summary>
	/// Creates and executes the specified controller.
	/// </summary>
	/// <param name="controllerMetaData">The controller meta-data.</param>
	/// <param name="resolver">The DI container resolver.</param>
	/// <param name="context">The context.</param>
	/// <param name="routeParameters">The route parameters.</param>
	Task<ControllerResponse?> Execute(IControllerMetaData controllerMetaData, IDIResolver resolver, HttpContext context,
		IDictionary<string, object>? routeParameters = null);
}