using System.Linq;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Meta.MetaStore;

namespace Simplify.Web.Controllers.ExecutionWorkOrder.BuildStages;

public class GlobalControllersBuilder : IWorkOrderBuildStage
{
	public void Execute(WorkOrderBuilder builder, HttpContext context)
	{
		if (builder.HttpStatusCode != null)
			return;

		builder.Controllers.AddRange(ControllersMetaStore.Current.GlobalControllers.Select(x => x.ToMatchedController()));
	}
}
