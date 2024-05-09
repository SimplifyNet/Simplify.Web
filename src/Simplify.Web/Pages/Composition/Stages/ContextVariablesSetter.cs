using Simplify.Web.Modules.Context;
using Simplify.Web.Pages.Composition;

namespace Simplify.Web.Pages.Composition.Stages;

public class ContextVariablesSetter(IWebContextProvider webContextProvider) : IPageCompositionStage
{
	/// <summary>
	/// The site variable name site URL.
	/// </summary>
	public const string VariableNameSiteUrl = "SV:SiteUrl";

	/// <summary>
	/// The site variable name site virtual path (returns '/yoursite' if your site is placed in virtual directory like http://yourdomain.com/yoursite/).
	/// </summary>
	public const string VariableNameSiteVirtualPath = "~";

	public void Execute(Modules.Data.IDataCollector dataCollector)
	{
		var context = webContextProvider.Get();

		dataCollector.Add(VariableNameSiteUrl, context.SiteUrl);
		dataCollector.Add(VariableNameSiteVirtualPath, context.VirtualPath);
	}
}