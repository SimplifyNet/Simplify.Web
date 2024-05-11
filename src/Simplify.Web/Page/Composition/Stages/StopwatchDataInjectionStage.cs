using Simplify.Web.Diagnostics.Measurement;

namespace Simplify.Web.Page.Composition.Stages;

public class StopwatchDataInjectionStage(IStopwatchProvider stopwatchProvider) : IPageCompositionStage
{
	public const string VariableNameExecutionTime = "SV:SiteExecutionTime";

	public void Execute(Modules.Data.IDataCollector dataCollector) =>
		dataCollector.Add(VariableNameExecutionTime, stopwatchProvider.StopAndGetMeasurement().ToString("mm\\:ss\\:fff"));
}