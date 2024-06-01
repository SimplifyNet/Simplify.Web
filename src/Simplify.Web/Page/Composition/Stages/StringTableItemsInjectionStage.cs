using Simplify.Web.Modules.Data;

namespace Simplify.Web.Page.Composition.Stages;

/// <summary>
/// Provides the string table items injection stage.
/// </summary>
/// <seealso cref="IPageCompositionStage" />
public class StringTableItemsInjectionStage(IStringTable stringTable) : IPageCompositionStage
{
	/// <summary>
	/// The string table prefix.
	/// </summary>
	public const string StringTablePrefix = "StringTable.";

	/// <summary>
	/// Executes this stage.
	/// </summary>
	/// <param name="dataCollector">The data collector.</param>
	public void Execute(IDataCollector dataCollector)
	{
		foreach (var item in stringTable.Items)
			dataCollector.Add(StringTablePrefix + item.Key, item.Value?.ToString());
	}
}