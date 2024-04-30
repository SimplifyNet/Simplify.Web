using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.WorkOrder;

namespace Simplify.Web.Controllers.WorkOrderProcessing;

public interface IWorkOrderProcessingStage
{
	public Task<ResponseBehavior> Execute(IExecutionWorkOrder workOrder, HttpResponse response, Action stopProcessing);
}