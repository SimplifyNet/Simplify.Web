using Simplify.Web.Controllers.WorkOrder.Construction;

namespace Simplify.Web.Controllers.Resolution;

public interface ICrsHandlingPipeline
{
	bool Execute(ControllerResolutionState state, WorkOrderBuilder builder);
}