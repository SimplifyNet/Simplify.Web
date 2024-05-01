using Simplify.Web.Controllers.ExecutionWorkOrder;

namespace Simplify.Web.Controllers.Resolution;

public interface ICrsHandlingPipeline
{
	bool Execute(ControllerResolutionState state, WorkOrderBuilder builder);
}