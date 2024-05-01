using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.Execution;
using Simplify.Web.Controllers.ExecutionWorkOrder;

namespace Simplify.Web.RequestHandling.Handlers;

public class ControllersHandler(IWorkOrderBuildDirector workOrderBuildDirector, IControllersExecutor controllersExecutor) : IRequestHandler
{
	public async Task Handle(HttpContext context, RequestHandlerAsync next)
	{
		var workOrder = workOrderBuildDirector.CreateWorkOrder(context);

		if (workOrder.HttpStatusCode != null)
		{
			context.Response.StatusCode = (int)workOrder.HttpStatusCode;
			return;
		}

		var result = await controllersExecutor.ExecuteAsync(workOrder.Controllers, context);

		if (result != ResponseBehavior.Default)
			return;

		await next();
	}
}