using Simplify.Web.Modules.Context;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Page.Composition.Stages;

public class ContextVariablesInjectionStage(IWebContextProvider webContextProvider) : IPageCompositionStage
{
	/// <summary>
	/// The site variable name site URL.
	/// </summary>
	public const string VariableNameSiteUrl = "SV:SiteUrl";

	/// <summary>
	/// The site variable name site virtual path (returns '/your-site' if your site is placed in virtual directory like http://yourdomain.com/yoursite/).
	/// </summary>
	public const string VariableNameSiteVirtualPath = "~";

	public void Execute(IDataCollector dataCollector)
	{
		var context = webContextProvider.Get();

		dataCollector.Add(VariableNameSiteUrl, context.SiteUrl);
		dataCollector.Add(VariableNameSiteVirtualPath, context.VirtualPath);
	}
}