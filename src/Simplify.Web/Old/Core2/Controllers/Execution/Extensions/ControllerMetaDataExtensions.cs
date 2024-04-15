using Simplify.Web.Http;
using Simplify.Web.Old.Core2.Controllers.Execution.Args;
using Simplify.Web.Old.Meta2;

namespace Simplify.Web.Old.Core2.Controllers.Execution.Extensions;

public static class ControllerMetaDataExtensions
{
	public static IControllerExecutionArgs ToControllerExecutionArgs(this IControllerMetaData metaData, IHttpContext context) =>
		new ControllerExecutionArgs(metaData, context);
}