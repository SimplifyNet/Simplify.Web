using System.Collections.Generic;
using Simplify.Web.Modules.Data;
using Simplify.Web.Page.Generation;

namespace Simplify.Web.Page.Composition;

public class PageComposer(IReadOnlyList<IPageCompositionStage> stages, IDataCollector dataCollector, IPageGenerator pageGenerator) : IPageComposer
{
	public string Compose()
	{
		foreach (var item in stages)
			item.Execute(dataCollector);

		return pageGenerator.Generate(dataCollector);
	}
}