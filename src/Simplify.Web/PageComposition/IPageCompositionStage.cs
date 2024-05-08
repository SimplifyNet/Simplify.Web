using Simplify.Web.Modules.Data;

namespace Simplify.Web.PageComposition;

public interface IPageCompositionStage
{
	void Execute(IDataCollector dataCollector);
}