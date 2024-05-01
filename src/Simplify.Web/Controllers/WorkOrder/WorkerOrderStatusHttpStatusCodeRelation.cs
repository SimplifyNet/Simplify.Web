using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Simplify.Web.Controllers.WorkOrder;

public class WorkerOrderStatusHttpStatusCodeRelation
{
	public static readonly IDictionary<WorkOrderStatus, int> Relation =
		new Dictionary<WorkOrderStatus, int>()
		{
			{ WorkOrderStatus.NoRoutedControllers, (int)HttpStatusCode.NotFound },
			{ WorkOrderStatus.Unauthorized, (int)HttpStatusCode.Unauthorized },
			{ WorkOrderStatus.Forbidden, (int)HttpStatusCode.Forbidden }
		};
}