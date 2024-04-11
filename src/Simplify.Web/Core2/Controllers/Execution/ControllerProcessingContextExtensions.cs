using Simplify.Web.Core2.Controllers.Processing;

namespace Simplify.Web.Core2.Controllers.Execution;

public static class ControllerProcessingContextExtensions
{
	public static IControllerExecutionArgs ToControllerExecutionArgs(this IControllerProcessingContext context) =>
		new ControllerExecutionArgs(context.Controller.MetaData, context.Context, context.Controller.RouteParameters);
}
