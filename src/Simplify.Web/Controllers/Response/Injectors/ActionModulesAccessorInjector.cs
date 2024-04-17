using Simplify.DI;
using Simplify.Web.Modules.Context;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Localization;
using Simplify.Web.Modules.Redirection;

namespace Simplify.Web.Controllers.Response.Injectors;

/// <summary>
/// Provides the builder for ActionModulesAccessor objects.
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