using System;
using System.Collections.Generic;
using Simplify.Web.Controllers.RouteMatching;

namespace Simplify.Web.Controllers.WorkOrder.Construction;

public class WorkOrderBuilder
{
	public List<IMatchedController> Controllers { get; set; } = [];

	public WorkOrderStatus Status { get; set; }

	public IExecutionWorkOrder Build()
	{
		throw new NotImplementedException();
	}
}