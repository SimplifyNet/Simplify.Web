using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Controllers.WorkOrder.Construction;

public interface IWorkOrderConstructionStage
{
	void Execute(WorkOrderBuilder builder, HttpContext context);
}