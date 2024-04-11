using System.Collections.Generic;
using Simplify.Web.Core2.Http;
using Simplify.Web.Meta2;

namespace Simplify.Web.Core2.Controllers.Execution;

/// <summary>
/// Provides controller execution arguments.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ControllerExecutionArgs"/> class.
/// </remarks>
/// <param name="controller">The controller metadata.</param>
/// <param name="context">The context.</param>
/// <param name="routeParameters">The route parameters.</param>
public class ControllerExecutionArgs(IControllerMetaData controller,
	IHttpContext context,
	IDictionary<string, object>? routeParameters = null) : IControllerExecutionArgs
{
	/// <summary>
	/// The controller metadata.
	/// </summary>
	public IControllerMetaData Controller { get; } = controller;

	/// <summary>
	/// The context.
	/// </summary>
	public IHttpContext Context { get; } = context;

	/// <summary>
	/// The route parameters.
	/// </summary>
	public IDictionary<string, object>? RouteParameters { get; } = routeParameters;
}