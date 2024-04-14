using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Simplify.DI;
using Simplify.Web.Meta;

namespace Simplify.Web.Core.Controllers.Execution;

/// <summary>
/// Provides controller execution arguments.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ControllerExecutionArgs"/> class.
/// </remarks>
/// <param name="controllerMetaData">The controller meta-data.</param>
/// <param name="resolver">The DI container resolver.</param>
/// <param name="context">The context.</param>
/// <param name="routeParameters">The route parameters.</param>
public class ControllerExecutionArgs(IControllerMetaData controllerMetaData,
	IDIResolver resolver,
	HttpContext context,
	IDictionary<string, object>? routeParameters = null) : IControllerExecutionArgs
{

	/// <summary>
	/// The controller meta-data.
	/// </summary>
	public IControllerMetaData ControllerMetaData { get; } = controllerMetaData;

	/// <summary>
	/// The DI container resolver.
	/// </summary>
	public IDIResolver Resolver { get; } = resolver;

	/// <summary>
	/// The context.
	/// </summary>
	public HttpContext Context { get; } = context;

	/// <summary>
	/// The route parameters.
	/// </summary>
	public IDictionary<string, object>? RouteParameters { get; } = routeParameters;
}