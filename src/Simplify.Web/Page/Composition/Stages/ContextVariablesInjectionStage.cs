using Simplify.Web.Modules.Context;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Page.Composition.Stages;

/// <summary>
/// Provides the context variables injection stage.
/// </summary>
/// <seealso cref="IPageCompositionStage" />
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

	/// <summary>
	/// Executes this stage.
	/// </summary>
	/// <param name="dataCollector">The data collector.</param>
	public void Execute(IDataCollector dataCollector)
	{
		var context = webContextProvider.Get();

		dataCollector.Add(VariableNameSiteUrl, context.SiteUrl);
		dataCollector.Add(VariableNameSiteVirtualPath, context.VirtualPath);
	}
}