using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Meta.MetaStore;
using Simplify.Web.Controllers.Resolution;
using Simplify.Web.Controllers.Resolution.Handling;

#pragma warning disable S3267

namespace Simplify.Web.Controllers.Execution.WorkOrder.BuildStages;

/// <summary>
/// Provides the routed controllers builder
/// </summary>
/// <seealso cref="IExecutionWorkOrderBuildStage" />
public class RoutedControllersBuilder(IControllerResolutionPipeline resolutionPipeline,
  ICrsHandlingPipeline crsHandlingPipeline) : IExecutionWorkOrderBuildStage
{
	/// <summary>
	/// Executes the build stage.
	/// </summary>
	/// <param name="builder">The builder.</param>
	/// <param name="context">The context.</param>
	public void Execute(ExecutionWorkOrderBuilder builder, HttpContext context)
	{
		foreach (var item in ControllersMetaStore.Current.RoutedControllers)
			if (crsHandlingPipeline.Execute(resolutionPipeline.Execute(item, context), builder))
				break;
	}
}