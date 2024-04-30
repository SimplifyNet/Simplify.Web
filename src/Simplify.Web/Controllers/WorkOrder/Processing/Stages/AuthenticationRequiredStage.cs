using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.WorkOrderProcessing;

namespace Simplify.Web.Controllers.WorkOrder.Processing.Stages;

public class AuthenticationRequiredStage : IWorkOrderProcessingStage
{
	public Task<ResponseBehavior> Execute(IExecutionWorkOrder workOrder, HttpResponse response, Action stopProcessing)
	{
		if (workOrder.Status == WorkOrderStatus.AuthenticationRequired)
		{
			response.StatusCode = 401;

			stopProcessing();
		}

		return Task.FromResult(ResponseBehavior.Default);
	}
}