using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Controllers.Execution.WorkOrder;

public interface IExecutionWorkOrderBuildStage
{
	void Execute(ExecutionWorkOrderBuilder builder, HttpContext context);
}