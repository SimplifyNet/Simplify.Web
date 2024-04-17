using System.Collections.Generic;
using Simplify.Web.Http;
using Simplify.Web.Meta;

namespace Simplify.Web.Controllers.Execution.Args;

public class ControllerExecutionArgs(IControllerMetadata controller,
	IHttpContext context,
	IDictionary<string, object>? routeParameters = null) : IControllerExecutionArgs
{
	public IControllerMetadata Controller { get; } = controller;

	public IHttpContext Context { get; } = context;

	public IDictionary<string, object>? RouteParameters { get; } = routeParameters;
}