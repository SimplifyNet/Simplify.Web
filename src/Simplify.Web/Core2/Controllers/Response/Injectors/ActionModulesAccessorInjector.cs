using Simplify.DI;
using Simplify.Web.Modules;
using Simplify.Web.Modules.Data;

namespace Simplify.Web.Core2.Controllers.Response.Injectors;

/// <summary>
/// Provides builder for ActionModulesAccessor objects.
/// </summary>
public abstract class ActionModulesAccessorInjector(IDIResolver resolver) : ModulesAccessorInjector(resolver)
{
	private readonly IDIResolver _resolver = resolver;

	/// <summary>
	/// Builds the modules accessor properties.
	/// </summary>
	/// <param name="modulesAccessor">The modules accessor.</param>
	protected void InjectActionModulesAccessorProperties(ActionModulesAccessor modulesAccessor)
	{
		InjectModulesAccessorProperties(modulesAccessor);

		modulesAccessor.Context = _resolver.Resolve<IWebContextProvider>().Get();
		modulesAccessor.DataCollector = _resolver.Resolve<IDataCollector>();
		modulesAccessor.Redirector = _resolver.Resolve<IRedirector>();
		modulesAccessor.LanguageManager = _resolver.Resolve<ILanguageManagerProvider>().Get();
		modulesAccessor.FileReader = _resolver.Resolve<IFileReader>();
	}
}