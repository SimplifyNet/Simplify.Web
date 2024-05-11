using Simplify.Web.Modules.Data;

namespace Simplify.Web.Page.Composition.Stages;

public class StringTableItemsInjectionStage(IStringTable stringTable) : IPageCompositionStage
{
	public const string StringTablePrefix = "StringTable.";

	public void Execute(IDataCollector dataCollector)
	{
		foreach (var item in stringTable.Items)
			dataCollector.Add(StringTablePrefix + item.Key, item.Value?.ToString());
	}
}