using Microsoft.AspNetCore.Http;

namespace Simplify.Web.Controllers.ExecutionWorkOrder;

public interface IWorkOrderBuildDirector
{
	IWorkOrder CreateWorkOrder(HttpContext context);
}