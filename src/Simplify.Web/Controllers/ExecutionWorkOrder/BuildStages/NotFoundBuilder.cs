using System.Net;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Meta.MetaStore;

namespace Simplify.Web.Controllers.ExecutionWorkOrder.BuildStages;

public class NotFoundBuilder : IWorkOrderBuildStage
{
	public void Execute(WorkOrderBuilder builder, HttpContext context)
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
