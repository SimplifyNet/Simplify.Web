using Simplify.DI;
using Simplify.Web.Old.Modules;
using Simplify.Web.Old.Modules.Data;

namespace Simplify.Web.Old.Core.PageAssembly;

/// <summary>
/// Provides web-page builder.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="PageBuilder" /> class.
/// </remarks>
/// <param name="templateFactory">The template factory.</param>
/// <param name="dataCollector">The data collector.</param>
public class PageBuilder(ITemplateFactory templateFactory, IDataCollector dataCollector) : IPageBuilder
{
	/// <summary>
	/// Builds a web page
	/// </summary>
	/// <param name="resolver">The DI container resolver.</param>
	/// <returns></returns>
	public string Build(IDIResolver resolver)
	{
		resolver.Resolve<IStringTableItemsSetter>().Set();
		resolver.Resolve<IContextVariablesSetter>().SetVariables(resolver);

		var tpl = templateFactory.Load(resolver.Resolve<IEnvironment>().MasterTemplateFileName);

		foreach (var item in dataCollector.Items.Keys)
			tpl.Set(item, dataCollector.Items[item]);

		return tpl.Get();
	}
}