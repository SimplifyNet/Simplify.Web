using Simplify.Web.Controllers.ExecutionWorkOrder;
using Simplify.Web.Controllers.Resolution;

namespace Simplify.Web.Controllers.ResolutionHandling;

public interface ICrsHandlingPipeline
{
	bool Execute(ControllerResolutionState state, WorkOrderBuilder builder);
}