using System.Collections.Generic;
using System.Net;

namespace Simplify.Web.Controllers.Execution.WorkOrder;

public interface IExecutionWorkOrder
{
	IReadOnlyList<IMatchedController> Controllers { get; }

	public HttpStatusCode? HttpStatusCode { get; }
}