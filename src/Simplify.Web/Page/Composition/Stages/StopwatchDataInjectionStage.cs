using Simplify.Web.Diagnostics.Measurements;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Page.Composition.Stages;

/// <summary>
/// Provides the stopwatch data injection stage.
/// </summary>
/// <seealso cref="IPageCompositionStage" />
public class StopwatchDataInjectionStage(IStopwatchProvider stopwatchProvider) : IPageCompositionStage
{
	/// <summary>
	/// The variable name execution time.
	/// </summary>
	public const string VariableNameExecutionTime = "SV:SiteExecutionTime";

	/// <summary>
	/// Executes this stage.
	/// </summary>
	/// <param name="dataCollector">The data collector.</param>
	public void Execute(IDataCollector dataCollector) =>
		dataCollector.Add(VariableNameExecutionTime, stopwatchProvider.StopAndGetMeasurement().ToString("mm\\:ss\\:fff"));
}