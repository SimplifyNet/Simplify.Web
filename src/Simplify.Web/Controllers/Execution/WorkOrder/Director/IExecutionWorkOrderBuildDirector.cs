using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Controllers.Execution.WorkOrder.Director;

public interface IExecutionWorkOrderBuildDirector
{
	IExecutionWorkOrder CreateWorkOrder(HttpContext context);
}