using Simplify.DI;
using Simplify.Web.Modules;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Core.AccessorsBuilding;

/// <summary>
/// Provides builder for ActionModulesAccessor objects
/// </summary>
public class ActionModulesAccessorBuilder : ModulesAccessorBuilder
{
	/// <summary>
	/// Builds the modules accessor properties.
	/// </summary>
	/// <param name="modulesAccessor">The modules accessor.</param>
	/// <param name="resolver">The DI container resolver.</param>
	protected void BuildActionModulesAccessorProperties(ActionModulesAccessor modulesAccessor, IDIResolver resolver)
	{
		BuildModulesAccessorProperties(modulesAccessor, resolver);

		modulesAccessor.Context = resolver.Resolve<IWebContextProvider>().Get();
		modulesAccessor.DataCollector = resolver.Resolve<IDataCollector>();
		modulesAccessor.Redirector = resolver.Resolve<IRedirector>();
		modulesAccessor.LanguageManager = resolver.Resolve<ILanguageManagerProvider>().Get();
		modulesAccessor.FileReader = resolver.Resolve<IFileReader>();
	}
}