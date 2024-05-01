using System.Collections.Generic;
using System.Net;

namespace Simplify.Web.Controllers.ExecutionWorkOrder;

public interface IWorkOrder
{
	IReadOnlyList<IMatchedController> Controllers { get; }

	public HttpStatusCode? HttpStatusCode { get; }
}