using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Controllers.Execution.WorkOrder;

/// <summary>
/// Represents an execution work order build stage
/// </summary>
public interface IExecutionWorkOrderBuildStage
{
	/// <summary>
	/// Executes the specified build stage.
	/// </summary>
	/// <param name="builder">The builder.</param>
	/// <param name="context">The context.</param>
	void Execute(ExecutionWorkOrderBuilder builder, HttpContext context);
}