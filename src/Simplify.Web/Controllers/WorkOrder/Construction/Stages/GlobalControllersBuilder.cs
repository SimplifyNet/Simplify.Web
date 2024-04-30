using System.Linq;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Extensions;
using Simplify.Web.Meta.Controllers;

namespace Simplify.Web.Controllers.WorkOrder.Construction.Stages;

public class GlobalControllersBuilder(IControllersMetaStore metaStore) : IWorkOrderConstructionStage
{
	public void Execute(WorkOrderBuilder builder, HttpContext context) =>
		builder.Controllers.AddRange(metaStore.GlobalControllers.Select(x => x.ToMatchedController()));
}
