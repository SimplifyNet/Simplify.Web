using System.Net;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Meta.MetaStore;

namespace Simplify.Web.Controllers.Execution.WorkOrder.BuildStages;

/// <summary>
/// Provides the not found cases builder
/// </summary>
/// <seealso cref="IExecutionWorkOrderBuildStage" />
public class NotFoundBuilder : IExecutionWorkOrderBuildStage
{
	/// <summary>
	/// Executes the build stage.
	/// </summary>
	/// <param name="builder">The builder.</param>
	/// <param name="context">The context.</param>
	public void Execute(ExecutionWorkOrderBuilder builder, HttpContext context)
	{
		if (builder.HttpStatusCode != null)
			return;

		if (builder.Controllers.Count > 0)
			return;

		if (ControllersMetaStore.Current.NotFoundController == null)
			builder.HttpStatusCode = HttpStatusCode.NotFound;
		else
			builder.Controllers.Add(ControllersMetaStore.Current.NotFoundController.ToMatchedController());
	}
}