using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Execution;
using Simplify.Web.Controllers.WorkOrder;
using Simplify.Web.Controllers.WorkOrder.Construction;

namespace Simplify.Web.RequestHandling.Handlers;

public class ControllersHandler(IWorkOrderConstructionDirector workOrderConstructionDirector, IControllersExecutor controllersExecutor) : IRequestHandler
{
	public async Task Handle(HttpContext context, RequestHandlerAsync next)
	{
		var workOrder = workOrderConstructionDirector.CreateWorkOrder(context);

		if (workOrder.Status != null)
		{
			context.Response.StatusCode = WorkerOrderStatusHttpStatusCodeRelation.Relation[workOrder.Status.Value];
			return;
		}

		var result = await controllersExecutor.ExecuteAsync(workOrder.Controllers, context);

		if (result != ResponseBehavior.Default)
			return;

		await next();
	}
}