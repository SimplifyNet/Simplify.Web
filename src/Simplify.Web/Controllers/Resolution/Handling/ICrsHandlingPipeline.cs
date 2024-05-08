using Simplify.Web.Controllers.ExecutionWorkOrder;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Controllers.Resolution.Handling;

public interface ICrsHandlingPipeline
{
	bool Execute(ControllerResolutionState state, WorkOrderBuilder builder);
}