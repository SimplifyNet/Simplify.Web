using Simplify.Web.Old.Core2.Controllers.Execution.Args;
using Simplify.Web.Old.Core2.Controllers.Processing.Context;

namespace Simplify.Web.Old.Core2.Controllers.Execution.Extensions;

public static class ControllerProcessingContextExtensions
{
	public static IControllerExecutionArgs ToControllerExecutionArgs(this IControllerProcessingContext context) =>
		new ControllerExecutionArgs(context.Controller.MetaData, context.Context, context.Controller.RouteParameters);
}
