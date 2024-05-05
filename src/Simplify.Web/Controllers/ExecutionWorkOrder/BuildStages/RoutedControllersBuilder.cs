using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Resolution;
using Simplify.Web.Controllers.ResolutionHandling;
using Simplify.Web.Controllers.Meta.MetaStore;

namespace Simplify.Web.Controllers.ExecutionWorkOrder.BuildStages;

public class RoutedControllersBuilder(IControllerResolutionPipeline resolutionPipeline,
  ICrsHandlingPipeline crsHandlingPipeline) : IWorkOrderBuildStage
{
	public void Execute(WorkOrderBuilder builder, HttpContext context)
	{
		foreach (var item in ControllersMetaStore.Current.RoutedControllers)
			if (crsHandlingPipeline.Execute(resolutionPipeline.Execute(item, context), builder))
				break;
	}
}
