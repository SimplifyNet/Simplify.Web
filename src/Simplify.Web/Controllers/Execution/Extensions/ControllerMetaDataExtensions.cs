using Simplify.Web.Controllers.Execution.Args;
using Simplify.Web.Http;
using Simplify.Web.Meta;

namespace Simplify.Web.Controllers.Execution.Extensions;

public static class ControllerMetaDataExtensions
{
	public static IControllerExecutionArgs ToControllerExecutionArgs(this IControllerMetadata metaData, IHttpContext context) =>
		new ControllerExecutionArgs(metaData, context);
}