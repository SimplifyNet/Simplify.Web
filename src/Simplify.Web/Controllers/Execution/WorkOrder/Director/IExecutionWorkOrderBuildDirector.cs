using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Controllers.Execution.WorkOrder.Director;

/// <summary>
/// Represents an execution work order build director
/// </summary>
public interface IExecutionWorkOrderBuildDirector
{
	/// <summary>
	/// Creates the work order.
	/// </summary>
	/// <param name="context">The context.</param>
	IExecutionWorkOrder CreateWorkOrder(HttpContext context);
}