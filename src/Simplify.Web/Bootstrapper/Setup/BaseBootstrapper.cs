using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Controllers.Meta.MetaStore;
using Simplify.Web.Settings;
using Simplify.Web.System;

namespace Simplify.Web.Bootstrapper.Setup;

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

		RegisterIController1Factory();
		RegisterController1PathParser();
		RegisterControllerExecutorResolver();
		RegisterControllerExecutorResolverExecutors();
		RegisterControllersExecutor();
		RegisterControllerResolutionPipeline();
		RegisterControllerResolutionPipelineStages();
		RegisterControllerResponseExecutor();
		RegisterControllerResponsePropertiesInjector();
		RegisterCrsHandlers();
		RegisterCrsHandlingPipeline();
		RegisterEnvironment();
		RegisterFileReader();
		RegisterLanguageManagerProvider();
		RegisterRequestHandlingPipeline();
		RegisterRequestHandlingPipelineHandlers();
		RegisterResponseWriter();
		RegisterRouteMatcherResolver();
		RegisterRouteMatcherResolverMatchers();
		RegisterSecurityChecker();
		RegisterSecurityRules();
		RegisterSimplifyWebSettings();
		RegisterStaticFile();
		RegisterStaticFileProcessingContextFactory();
		RegisterStaticFileRequestHandlingPipeline();
		RegisterStaticFileRequestHandlingPipelineHandlers();
		RegisterStopwatchProvider();
		RegisterStringTable();
		RegisterTemplateFactory();
		RegisterWebContextProvider();
		RegisterWorkOrderBuildDirector();
		RegisterWorkOrderBuildDirectorStages();

		var typesToIgnore = SimplifyWebTypesFinder.GetIgnoredIocRegistrationTypes();

		RegisterControllers(typesToIgnore);
	}

	/// <summary>
	/// Registers the controllers.
	/// </summary>
	/// <param name="typesToIgnore">The types to ignore.</param>
	public virtual void RegisterControllers(IEnumerable<Type> typesToIgnore)
	{
		foreach (var controller in ControllersMetaStore.Current.AllControllers
			.Where(controllerMetaData => typesToIgnore.All(x => x != controllerMetaData.ControllerType)))
			BootstrapperFactory.ContainerProvider.Register(controller.ControllerType, LifetimeType.Transient);
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