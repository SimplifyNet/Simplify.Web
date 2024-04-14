using Simplify.Web.Core2.Controllers.Execution.Args;
using Simplify.Web.Meta2;

namespace Simplify.Web.Core2.Controllers.Execution.Extensions;

public static class ControllerMetaDataExtensions
{
	public static IControllerExecutionArgs ToControllerExecutionArgs(this IControllerMetaData metaData, IHttpContext context) =>
		new ControllerExecutionArgs(metaData, context);
}
