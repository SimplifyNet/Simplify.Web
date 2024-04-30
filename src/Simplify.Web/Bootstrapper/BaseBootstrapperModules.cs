using System;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Modules.ApplicationEnvironment;
using Simplify.Web.Modules.Context;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Localization;
using Simplify.Web.Settings;

namespace Simplify.Web.Bootstrapper;

/// <summary>
/// Provides the bootstrapper modules registrations.
/// </summary>
public partial class BaseBootstrapper
{
	/// <summary>
	/// Registers the environment.
	/// </summary>
	public virtual void RegisterEnvironment()
	{
		if (TypesToExclude.Contains(typeof(IEnvironment)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IEnvironment>(r =>
			new Modules.ApplicationEnvironment.Environment(AppDomain.CurrentDomain.BaseDirectory ?? "", r.Resolve<ISimplifyWebSettings>()));
	}

	/// <summary>
	/// Registers the language manager provider.
	/// </summary>
	public virtual void RegisterLanguageManagerProvider()
	{
		if (TypesToExclude.Contains(typeof(ILanguageManagerProvider)))
			return;

		BootstrapperFactory.ContainerProvider.Register<ILanguageManagerProvider>(r =>
			new LanguageManagerProvider(r.Resolve<ISimplifyWebSettings>()));
	}

	/// <summary>
	/// Registers the template factory.
	/// </summary>
	public virtual void RegisterTemplateFactory()
	{
		if (TypesToExclude.Contains(typeof(ITemplateFactory)))
			return;

		BootstrapperFactory.ContainerProvider.Register<ITemplateFactory>(r =>
		{
			var settings = r.Resolve<ISimplifyWebSettings>();

			return new TemplateFactory(
				r.Resolve<IEnvironment>(),
				r.Resolve<ILanguageManagerProvider>(),
				settings.DefaultLanguage,
				settings.TemplatesMemoryCache,
				settings.LoadTemplatesFromAssembly);
		});
	}

	/// <summary>
	/// Registers the web context provider.
	/// </summary>
	public virtual void RegisterWebContextProvider()
	{
		if (TypesToExclude.Contains(typeof(IWebContextProvider)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IWebContextProvider, WebContextProvider>();
	}
}