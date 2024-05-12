using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Controllers.Resolution.Handling;

public interface ICrsHandlingPipeline
{
	bool Execute(IControllerResolutionState state, ExecutionWorkOrderBuilder builder);
}