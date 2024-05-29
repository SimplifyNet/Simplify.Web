using Simplify.Web.Diagnostics.Measurements;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Page.Composition.Stages;

public class StopwatchDataInjectionStage(IStopwatchProvider stopwatchProvider) : IPageCompositionStage
{
	public const string VariableNameExecutionTime = "SV:SiteExecutionTime";

	public void Execute(IDataCollector dataCollector) =>
		dataCollector.Add(VariableNameExecutionTime, stopwatchProvider.StopAndGetMeasurement().ToString("mm\\:ss\\:fff"));
}