using Simplify.Web.Controllers.Execution.Args;
using Simplify.Web.Controllers.Processing.Context;

namespace Simplify.Web.Controllers.Execution.Extensions;

public static class ControllerProcessingContextExtensions
{
	public static IControllerExecutionArgs ToControllerExecutionArgs(this IControllerProcessingContext context) =>
		new ControllerExecutionArgs(context.Controller.Controller, context.Context, context.Controller.RouteParameters);
}