using Simplify.Web.Modules.Data;

namespace Simplify.Web.Page.Composition;

public interface IPageCompositionStage
{
	void Execute(IDataCollector dataCollector);
}