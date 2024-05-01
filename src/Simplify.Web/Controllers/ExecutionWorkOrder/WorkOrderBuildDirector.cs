using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Controllers.ExecutionWorkOrder
{
	public class WorkOrderBuildDirector(IReadOnlyList<IWorkOrderBuildStage> stages) : IWorkOrderBuildDirector
	{
		public IWorkOrder CreateWorkOrder(HttpContext context)
		{
			var builder = new WorkOrderBuilder();

			foreach (var item in stages)
				item.Execute(builder, context);

			return builder.Build();
		}
	}
}