using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Controllers.ExecutionWorkOrder;

public interface IWorkOrderBuildStage
{
	void Execute(WorkOrderBuilder builder, HttpContext context);
}