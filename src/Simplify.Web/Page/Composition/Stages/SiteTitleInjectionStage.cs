using Simplify.Web.Modules.Context;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Page.Composition.Stages;

/// <summary>
/// Provides the site title injection stage.
/// </summary>
/// <seealso cref="IPageCompositionStage" />
public class SiteTitleInjectionStage(IWebContextProvider webContextProvider, IStringTable stringTable) : IPageCompositionStage
{
	/// <summary>
	/// The site title string table variable name.
	/// </summary>
	public const string SiteTitleStringTableVariableName = "SiteTitle";

	/// <summary>
	/// Executes this stage.
	/// </summary>
	/// <param name="dataCollector">The data collector.</param>
	public void Execute(IDataCollector dataCollector)
	{
		var siteTitle = stringTable.GetItem(SiteTitleStringTableVariableName);

		if (string.IsNullOrEmpty(siteTitle))
			return;

		var context = webContextProvider.Get();
		var currentPath = context.Request.Path.Value;

		if (string.IsNullOrEmpty(currentPath) ||
			currentPath == "/" ||
			currentPath!.StartsWith("/?") ||
			!dataCollector.IsDataExist(dataCollector.TitleVariableName))
			dataCollector.Add(dataCollector.TitleVariableName, siteTitle);
		else
			dataCollector.Add(dataCollector.TitleVariableName, " - " + siteTitle);
	}
}