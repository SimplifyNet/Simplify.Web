using System.Collections.Generic;
using System.Net;

namespace Simplify.Web.Controllers.ExecutionWorkOrder;

public class WorkOrder(IReadOnlyList<IMatchedController> controllers, HttpStatusCode? httpStatusCode = null) : IWorkOrder
{
	public IReadOnlyList<IMatchedController> Controllers { get; } = controllers;

	public HttpStatusCode? HttpStatusCode { get; } = httpStatusCode;
}