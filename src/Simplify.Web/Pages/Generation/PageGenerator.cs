using Simplify.Web.Modules.ApplicationEnvironment;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Pages.Generation;

/// <summary>
/// Provides the web-page HTML generator.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PageGenerator" /> class.
/// </remarks>
/// <param name="templateFactory">The template factory.</param>
/// <param name="dataCollector">The data collector.</param>
/// <param name="IDynamicEnvironment">The environment.</param>
public class PageGenerator(ITemplateFactory templateFactory, IDynamicEnvironment environment) : IPageGenerator
{
	/// <summary>
	/// Generates the web page HTML code
	/// </summary>
	public string Generate(IDataCollector dataCollector)
	{
		var tpl = templateFactory.Load(environment.MasterTemplateFileName);

		foreach (var item in dataCollector.Items.Keys)
			tpl.Set(item, dataCollector.Items[item]);

		return tpl.Get();
	}
}