using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Controllers.Meta.MetaStore;
using Simplify.Web.System;
using Simplify.Web.Views.Meta;

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
	/// Registers the DI container scope resolver
	/// </summary>
	public static void RegisterDIResolver() => BootstrapperFactory.ContainerProvider.Register(r => r);

	/// <summary>
	/// Registers the types in container.
	/// </summary>
	public void Register(IEnumerable<Type>? typesToExclude = null)
	{
		if (typesToExclude != null)
			TypesToExclude = typesToExclude;

		RegisterDIResolver();

		// Registering non Simplify.Web types
		RegisterConfiguration();

		// Registering Simplify.Web types

		RegisterController1Factory();
		RegisterController2Factory();
		RegisterControllerExecutorResolver();
		RegisterControllerExecutorResolverExecutors();
		RegisterControllersExecutor();
		RegisterControllerResolutionPipeline();
		RegisterControllerResolutionPipelineStages();
		RegisterControllerResponseExecutor();
		RegisterCrsHandlers();
		RegisterCrsHandlingPipeline();
		RegisterDataCollector();
		RegisterDefaultModelBinders();
		RegisterDefaultModelValidators();
		RegisterDynamicEnvironment();
		RegisterEnvironment();
		RegisterExecutionWorkOrderBuildDirector();
		RegisterExecutionWorkOrderBuildDirectorStages();
		RegisterFileReader();
		RegisterLanguageManagerProvider();
		RegisterListsGenerator();
		RegisterModelHandler();
		RegisterPageComposer();
		RegisterPageCompositionStages();
		RegisterPageGenerator();
		RegisterRedirector();
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
		RegisterViewFactory();
		RegisterWebContextProvider();

		var typesToIgnore = SimplifyWebTypesFinder
			.GetIgnoredIocRegistrationTypes()
			.ToList();

		RegisterControllers(typesToIgnore);
		RegisterViews(typesToIgnore);
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
	/// Registers the views.
	/// </summary>
	/// <param name="typesToIgnore">The types to ignore.</param>
	public virtual void RegisterViews(IEnumerable<Type> typesToIgnore)
	{
		foreach (var viewType in ViewsMetaStore.Current.ViewsTypes
			.Where(viewType => typesToIgnore.All(x => x != viewType)))
			BootstrapperFactory.ContainerProvider.Register(viewType, LifetimeType.Transient);
	}
}