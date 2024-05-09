using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Simplify.Web.Controllers.Execution.WorkOrder;

public class ExecutionWorkOrderBuilder
{
	public List<IMatchedController> Controllers { get; } = [];

	public HttpStatusCode? HttpStatusCode { get; set; }

	public IExecutionWorkOrder Build() =>
		new ExecutionWorkOrder(
			Controllers
				.SortByRunPriority()
				.ToList()
				.AsReadOnly(),
			HttpStatusCode);
}