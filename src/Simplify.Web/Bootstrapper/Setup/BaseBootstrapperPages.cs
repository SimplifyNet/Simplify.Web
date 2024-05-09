using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Diagnostics.Measurement;
using Simplify.Web.Modules.ApplicationEnvironment;
using Simplify.Web.Modules.Context;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Localization;
using Simplify.Web.Pages.Composition;
using Simplify.Web.Pages.Composition.Stages;
using Simplify.Web.Pages.Generation;

namespace Simplify.Web.Bootstrapper.Setup;

/// <summary>
/// Provides the bootstrapper pages registrations.
/// </summary>
public partial class BaseBootstrapper
{
	/// <summary>
	/// Registers the page generator.
	/// </summary>
	public virtual void RegisterPageGenerator()
	{
		if (TypesToExclude.Contains(typeof(IPageGenerator)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IPageGenerator, PageGenerator>();
	}

	public virtual void RegisterPageComposer()
	{
		if (TypesToExclude.Contains(typeof(IPageComposer)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IPageComposer, PageComposer>();
	}

	public virtual void RegisterPageCompositionStages()
	{
		if (TypesToExclude.Contains(typeof(IReadOnlyList<IPageCompositionStage>)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IReadOnlyList<IPageCompositionStage>>(r =>
			new List<IPageCompositionStage>
			{
				new StringTableItemsInjectionStage(r.Resolve<IStringTable>()),
				new LanguageInjectionStage(r.Resolve<ILanguageManagerProvider>()),
				new EnvironmentVariablesInjectionStage(r.Resolve<IDynamicEnvironment>()),
				new ContextVariablesInjectionStage(r.Resolve<IWebContextProvider>()),
				new SiteTitleInjectionStage(r.Resolve<IWebContextProvider>(), r.Resolve<IStringTable>()),
				new StopwatchDataInjectionStage(r.Resolve<IStopwatchProvider>())
			});
	}
}