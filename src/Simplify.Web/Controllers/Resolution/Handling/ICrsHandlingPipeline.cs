using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Controllers.Resolution.Handling;

/// <summary>
/// Represents a CRS handling pipeline.
/// </summary>
public interface ICrsHandlingPipeline
{
	/// <summary>
	/// Executes this pipeline.
	/// </summary>
	/// <param name="state">The state.</param>
	/// <param name="builder">The builder.</param>
	bool Execute(IControllerResolutionState state, ExecutionWorkOrderBuilder builder);
}