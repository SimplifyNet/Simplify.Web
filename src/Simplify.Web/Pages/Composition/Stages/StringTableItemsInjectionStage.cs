using System.Collections.Generic;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Pages.Composition.Stages;

public class StringTableItemsInjectionStage(IStringTable stringTable) : IPageCompositionStage
{
	private const string StringTablePrefix = "StringTable.";

	public void Execute(IDataCollector dataCollector)
	{
		foreach (var item in (IDictionary<string, object>)stringTable.Items)
			dataCollector.Add(StringTablePrefix + item.Key, item.Value.ToString());
	}
}