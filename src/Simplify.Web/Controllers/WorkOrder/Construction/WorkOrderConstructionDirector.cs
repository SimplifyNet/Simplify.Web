using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Controllers.WorkOrder.Construction
{
	public class WorkOrderConstructionDirector(IReadOnlyList<IWorkOrderConstructionStage> stages) : IWorkOrderConstructionDirector
	{
		public IExecutionWorkOrder CreateWorkOrder(HttpContext context)
		{
			var builder = new WorkOrderBuilder();

			foreach (var item in stages)
				item.Execute(builder, context);

			return builder.Build();
		}
	}
}