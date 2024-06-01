using Simplify.Web.Modules.Data;

namespace Simplify.Web.Page.Composition;

/// <summary>
/// Represents a page composition stage.
/// </summary>
public interface IPageCompositionStage
{
	/// <summary>
	/// Executes this stage.
	/// </summary>
	/// <param name="dataCollector">The data collector.</param>
	void Execute(IDataCollector dataCollector);
}