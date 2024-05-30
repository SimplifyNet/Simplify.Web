using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Controllers.Execution.WorkOrder.Director;

/// <summary>
/// Provides the execution work order build director
/// </summary>
/// <seealso cref="IExecutionWorkOrderBuildDirector" />
public class ExecutionWorkOrderBuildDirector(IReadOnlyList<IExecutionWorkOrderBuildStage> stages) : IExecutionWorkOrderBuildDirector
{
	/// <summary>
	/// Creates the work order.
	/// </summary>
	/// <param name="context">The context.</param>
	public IExecutionWorkOrder CreateWorkOrder(HttpContext context)
	{
		var builder = new ExecutionWorkOrderBuilder();

		foreach (var item in stages)
			item.Execute(builder, context);

		return builder.Build();
	}
}