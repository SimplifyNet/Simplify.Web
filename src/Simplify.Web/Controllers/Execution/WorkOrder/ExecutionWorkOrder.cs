using System.Collections.Generic;
using System.Net;

namespace Simplify.Web.Controllers.Execution.WorkOrder;

public class ExecutionWorkOrder(IReadOnlyList<IMatchedController> controllers, HttpStatusCode? httpStatusCode = null) : IExecutionWorkOrder
{
	public IReadOnlyList<IMatchedController> Controllers { get; } = controllers;

	public HttpStatusCode? HttpStatusCode { get; } = httpStatusCode;
}