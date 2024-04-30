using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Controllers.WorkOrder.Construction;

public interface IWorkOrderConstructionDirector
{
	IExecutionWorkOrder CreateWorkOrder(HttpContext context);
}