using System.Linq;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Meta.MetaStore;

namespace Simplify.Web.Controllers.Execution.WorkOrder.BuildStages;

/// <summary>
/// Provides the global controllers builder
/// </summary>
/// <seealso cref="IExecutionWorkOrderBuildStage" />
public class GlobalControllersBuilder : IExecutionWorkOrderBuildStage
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

		builder.Controllers.AddRange(ControllersMetaStore.Current.GlobalControllers.Select(x => x.ToMatchedController()));
	}
}