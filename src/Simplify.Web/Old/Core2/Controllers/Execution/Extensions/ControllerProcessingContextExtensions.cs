using Simplify.Web.Controllers.Processing.Context;
using Simplify.Web.Old.Core2.Controllers.Execution.Args;

namespace Simplify.Web.Old.Core2.Controllers.Execution.Extensions;

public static class ControllerProcessingContextExtensions
{
	public static IControllerExecutionArgs ToControllerExecutionArgs(this IControllerProcessingContext context) =>
		new ControllerExecutionArgs(context.Controller.MetaData, context.Context, context.Controller.RouteParameters);
}
