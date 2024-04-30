using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.Execution.Args;

public class ControllerExecutionArgs(IControllerMetadata controller,
	HttpContext context,
	IReadOnlyDictionary<string, object>? routeParameters = null) : IControllerExecutionArgs
{
	public IControllerMetadata Controller { get; } = controller;

	public HttpContext Context { get; } = context;

	public IReadOnlyDictionary<string, object>? RouteParameters { get; } = routeParameters;
}