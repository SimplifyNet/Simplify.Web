using System.Collections.Generic;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.PageComposition;

public class PageComposer(IReadOnlyList<IPageCompositionStage> stages, IDataCollector dataCollector, IPageGenerator pageGenerator) : IPageComposer
{
	public string Compose()
	{
		foreach (var item in stages)
			item.Execute(dataCollector);

		return pageGenerator.Generate(dataCollector);
	}
}