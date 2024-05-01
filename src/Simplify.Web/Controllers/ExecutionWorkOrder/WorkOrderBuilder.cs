using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Simplify.Web.Controllers.ExecutionWorkOrder;

public class WorkOrderBuilder
{
	public List<IMatchedController> Controllers { get; set; } = [];

	public HttpStatusCode? HttpStatusCode { get; set; }

	public IWorkOrder Build() =>
		new WorkOrder(
			Controllers
				.SortByRunPriority()
				.ToList()
				.AsReadOnly(),
			HttpStatusCode);
}