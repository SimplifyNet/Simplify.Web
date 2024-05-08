using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Resolution;
using Simplify.Web.Controllers.Meta.MetaStore;
using Simplify.Web.Controllers.Resolution.Handling;

namespace Simplify.Web.Controllers.Execution.WorkOrder.BuildStages;

public class RoutedControllersBuilder(IControllerResolutionPipeline resolutionPipeline,
  ICrsHandlingPipeline crsHandlingPipeline) : IExecutionWorkOrderBuildStage
{
	public void Execute(ExecutionWorkOrderBuilder builder, HttpContext context)
	{
		foreach (var item in ControllersMetaStore.Current.RoutedControllers)
			if (crsHandlingPipeline.Execute(resolutionPipeline.Execute(item, context), builder))
				break;
	}
}
