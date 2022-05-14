using Simplify.DI;
using Simplify.Web.Modules;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Data.Html;

namespace Simplify.Web.Core.AccessorsBuilding;

/// <summary>
/// Provides builder for ModulesAccessor objects
/// </summary>
public class ModulesAccessorBuilder : ViewAccessorBuilder
{
	/// <summary>
	/// Builds the modules accessor properties.
	/// </summary>
	/// <param name="modulesAccessor">The modules accessor.</param>
	/// <param name="resolver">The DI container resolver.</param>
	protected void BuildModulesAccessorProperties(ModulesAccessor modulesAccessor, IDIResolver resolver)
	{
		BuildViewAccessorProperties(modulesAccessor, resolver);

		modulesAccessor.Environment = resolver.Resolve<IEnvironment>();

		var stringTable = resolver.Resolve<IStringTable>();
		modulesAccessor.StringTable = stringTable.Items;
		modulesAccessor.StringTableManager = stringTable;

		modulesAccessor.TemplateFactory = resolver.Resolve<ITemplateFactory>();

		var htmlWrapper = new HtmlWrapper
		{
			ListsGenerator = resolver.Resolve<IListsGenerator>()
		};

		modulesAccessor.Html = htmlWrapper;
	}
}