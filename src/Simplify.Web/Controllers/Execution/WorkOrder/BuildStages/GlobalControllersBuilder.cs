using System.Linq;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Meta.MetaStore;

namespace Simplify.Web.Controllers.Execution.WorkOrder.BuildStages;

public class GlobalControllersBuilder : IExecutionWorkOrderBuildStage
{
	public void Execute(ExecutionWorkOrderBuilder builder, HttpContext context)
	{
		if (builder.HttpStatusCode != null)
			return;

		builder.Controllers.AddRange(ControllersMetaStore.Current.GlobalControllers.Select(x => x.ToMatchedController()));
	}
}
