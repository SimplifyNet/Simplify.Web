using System.Collections.Generic;
using System.Net;
using Simplify.Web.Controllers.RouteMatching;

namespace Simplify.Web.Controllers.ExecutionWorkOrder;

public interface IWorkOrder
{
	IReadOnlyList<IMatchedController> Controllers { get; }

	public HttpStatusCode? HttpStatusCode { get; }
}