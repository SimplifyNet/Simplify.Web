using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Settings;

namespace Simplify.Web.Bootstrapper;

/// <summary>
/// Provides the base and default Simplify.Web bootstrapper.
/// </summary>
public partial class BaseBootstrapper
{
	/// <summary>
	/// Provides the `Simplify.Web` types to exclude from registrations.
	/// </summary>
	protected IEnumerable<Type> TypesToExclude { get; private set; } = [];

	/// <summary>
	/// Registers the types in container.
	/// </summary>
	public void Register(IEnumerable<Type>? typesToExclude = null)
	{
		if (typesToExclude != null)
			TypesToExclude = typesToExclude;

		// Registering non Simplify.Web types
		RegisterConfiguration();

		// Registering Simplify.Web core types

		RegisterEnvironment();
		RegisterFileReader();
		RegisterLanguageManagerProvider();
		RegisterSimplifyWebSettings();
		RegisterStopwatchProvider();
		RegisterStringTable();
		RegisterTemplateFactory();
		RegisterWebContextProvider();
	}

	/// <summary>
	/// Registers the Simplify.Web settings.
	/// </summary>
	public virtual void RegisterSimplifyWebSettings()
	{
		if (TypesToExclude.Contains(typeof(ISimplifyWebSettings)))
			return;

		BootstrapperFactory.ContainerProvider.Register<ISimplifyWebSettings, SimplifyWebSettings>(LifetimeType.Singleton);
	}
}