using System.Collections.Generic;
using System.Net;
using Simplify.Web.Controllers.RouteMatching;

namespace Simplify.Web.Controllers.ExecutionWorkOrder;

public class WorkOrderBuilder
{
	public List<IMatchedController> Controllers { get; set; } = [];

	public HttpStatusCode? HttpStatusCode { get; set; }

	public IWorkOrder Build() => new WorkOrder(Controllers, HttpStatusCode);
}