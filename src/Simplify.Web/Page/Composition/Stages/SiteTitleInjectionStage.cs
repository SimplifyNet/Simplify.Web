using Simplify.Web.Modules.Context;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Page.Composition.Stages;

public class SiteTitleInjectionStage(IWebContextProvider webContextProvider, IStringTable stringTable) : IPageCompositionStage
{
	/// <summary>
	/// The site title string table variable name.
	/// </summary>
	public const string SiteTitleStringTableVariableName = "SiteTitle";

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