using Simplify.Web.Modules.Data;

namespace Simplify.Web.Pages.Composition;

public interface IPageCompositionStage
{
	void Execute(IDataCollector dataCollector);
}