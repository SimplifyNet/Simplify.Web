using Simplify.DI;
using Simplify.Web.Modules;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Data.Html;

namespace Simplify.Web.Core2.Controllers.Response.Injectors;

/// <summary>
/// Provides builder for ModulesAccessor objects.
/// </summary>
public abstract class ModulesAccessorInjector(IDIResolver resolver) : ViewAccessorInjector(resolver)
{
	private readonly IDIResolver _resolver = resolver;

	/// <summary>
	/// Builds the modules accessor properties.
	/// </summary>
	/// <param name="modulesAccessor">The modules accessor.</param>
	protected void InjectModulesAccessorProperties(ModulesAccessor modulesAccessor)
	{
		InjectViewAccessorProperties(modulesAccessor);

		modulesAccessor.Environment = _resolver.Resolve<IEnvironment>();

		var stringTable = _resolver.Resolve<IStringTable>();
		modulesAccessor.StringTableManager = stringTable;

		modulesAccessor.TemplateFactory = _resolver.Resolve<ITemplateFactory>();

		var htmlWrapper = new HtmlWrapper
		{
			ListsGenerator = _resolver.Resolve<IListsGenerator>()
		};

		modulesAccessor.Html = htmlWrapper;
	}
}