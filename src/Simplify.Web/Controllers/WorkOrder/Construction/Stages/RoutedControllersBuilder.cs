using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Resolution;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.WorkOrder.Construction.Stages;

public class RoutedControllersBuilder(IControllersMetaStore metaStore,
 IControllerResolutionPipeline resolutionPipeline,
  ICrsHandlingPipeline crsHandlingPipeline) : IWorkOrderConstructionStage
{
	public void Execute(WorkOrderBuilder builder, HttpContext context)
	{
		foreach (var item in metaStore.RoutedControllers)
		{
			var handlingResult = crsHandlingPipeline.Execute(resolutionPipeline.Execute(item, context), builder);

			if (handlingResult)
				break;
		}
	}
}
