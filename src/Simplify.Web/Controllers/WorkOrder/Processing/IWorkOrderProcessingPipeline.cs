using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simplify.Web.Controllers.WorkOrder;

namespace Simplify.Web.Controllers.WorkOrderProcessing;

public interface IWorkOrderProcessingPipeline
{
	Task Execute(IExecutionWorkOrder workOrder, HttpResponse response);
}