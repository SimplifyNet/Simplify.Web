using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Execution.Args;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.Execution.Extensions;

public static class ControllerMetaDataExtensions
{
	public static IControllerExecutionArgs ToControllerExecutionArgs(this IControllerMetadata metaData, HttpContext context) =>
		new ControllerExecutionArgs(metaData, context);
}