using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.WorkOrder;

namespace Simplify.Web.Controllers.WorkOrderProcessing;

public class WorkOrderProcessingPipeline(IReadOnlyList<IWorkOrderProcessingStage> stages) : IWorkOrderProcessingPipeline
{
	public async Task Execute(IExecutionWorkOrder workOrder, HttpResponse response)
	{
		var stopProcessing = false;

		foreach (var item in stages)
		{
			await item.Execute(workOrder, response, StopProcessing);

			if (stopProcessing)
				break;
		}

		return;

		void StopProcessing() => stopProcessing = true;
	}
}