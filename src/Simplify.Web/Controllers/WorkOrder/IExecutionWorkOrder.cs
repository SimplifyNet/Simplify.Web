using System.Collections.Generic;
using Simplify.Web.Controllers.RouteMatching;

namespace Simplify.Web.Controllers.WorkOrder;

public interface IExecutionWorkOrder
{
	IReadOnlyList<IMatchedController> Controllers { get; }

	public WorkOrderStatus? Status { get; }
}