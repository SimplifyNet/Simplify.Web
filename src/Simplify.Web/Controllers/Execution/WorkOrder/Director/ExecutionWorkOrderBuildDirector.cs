using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Controllers.Execution.WorkOrder.Director;

public class ExecutionWorkOrderBuildDirector(IReadOnlyList<IExecutionWorkOrderBuildStage> stages) : IExecutionWorkOrderBuildDirector
{
	public IExecutionWorkOrder CreateWorkOrder(HttpContext context)
	{
		var builder = new ExecutionWorkOrderBuilder();

		foreach (var item in stages)
			item.Execute(builder, context);

		return builder.Build();
	}
}