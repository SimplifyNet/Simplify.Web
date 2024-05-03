using System.Linq;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.ExecutionWorkOrder.BuildStages;

public class GlobalControllersBuilder : IWorkOrderBuildStage
{
	public void Execute(WorkOrderBuilder builder, HttpContext context) =>
		builder.Controllers.AddRange(ControllersMetaStore.Current.GlobalControllers.Select(x => x.ToMatchedController()));
}
