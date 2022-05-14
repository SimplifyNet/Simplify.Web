using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Simplify.DI;
using Simplify.Web.Core;
using Simplify.Web.Core.Controllers;
using Simplify.Web.Core.Controllers.Execution;
using Simplify.Web.Core.Controllers.Execution.Building;
using Simplify.Web.Core.PageAssembly;
using Simplify.Web.Core.StaticFiles;
using Simplify.Web.Core.Views;
using Simplify.Web.Diagnostics.Measurement;
using Simplify.Web.Meta;
using Simplify.Web.Model;
using Simplify.Web.Model.Binding;
using Simplify.Web.Model.Validation;
using Simplify.Web.Modules;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Data.Html;
using Simplify.Web.Routing;
using Simplify.Web.Settings;

namespace Simplify.Web.Bootstrapper;

/// <summary>
/// Base and default Simplify.Web bootstrapper
/// </summary>
// ReSharper disable once ClassTooBig
public class BaseBootstrapper
{
	/// <summary>
	/// Registers the types in container.
	/// </summary>
	// ReSharper disable once MethodTooLong
	public void Register()
	{
		// Registering non Simplify.Web types

		RegisterConfiguration();

		// Registering Simplify.Web core types

		RegisterControllersMetaStore();
		RegisterViewsMetaStore();

		RegisterSimplifyWebSettings();
		RegisterViewFactory();
		RegisterControllerFactory();
		RegisterControllerPathParser();
		RegisterRouteMatcher();
		RegisterControllersAgent();
		RegisterControllerResponseBuilder();
		RegisterControllerExecutor();
		RegisterControllersProcessor();
		RegisterEnvironment();
		RegisterLanguageManagerProvider();
		RegisterTemplateFactory();
		RegisterFileReader();
		RegisterStringTable();
		RegisterDataCollector();
		RegisterListsGenerator();
		RegisterStringTableItemsSetter();
		RegisterPageBuilder();
		RegisterResponseWriter();
		RegisterPageProcessor();
		RegisterControllersRequestHandler();
		RegisterStaticFileResponseFactory();
		RegisterStaticFileHandler();
		RegisterStaticFilesRequestHandler();
		RegisterRequestHandler();
		RegisterStopwatchProvider();
		RegisterContextVariablesSetter();
		RegisterWebContextProvider();
		RegisterRedirector();
		RegisterModelHandler();
		RegisterDefaultModelBinders();
		RegisterDefaultModelValidators();

		var typesToIgnore = SimplifyWebTypesFinder.GetTypesToIgnore();

		RegisterControllers(typesToIgnore);
		RegisterViews(typesToIgnore);
	}

	/// <summary>
	/// Registers the controllers.
	/// </summary>
	/// <param name="typesToIgnore">The types to ignore.</param>
	public virtual void RegisterControllers(IEnumerable<Type> typesToIgnore)
	{
		foreach (var controllerMetaData in ControllersMetaStore.Current.ControllersMetaData
			         .Where(controllerMetaData => typesToIgnore.All(x => x != controllerMetaData.ControllerType)))
		{
			BootstrapperFactory.ContainerProvider.Register(controllerMetaData.ControllerType, LifetimeType.Transient);
		}
	}

	/// <summary>
	/// Registers the views.
	/// </summary>
	/// <param name="typesToIgnore">The types to ignore.</param>
	public virtual void RegisterViews(IEnumerable<Type> typesToIgnore)
	{
		foreach (var viewType in ViewsMetaStore.Current.ViewsTypes.Where(viewType => typesToIgnore.All(x => x != viewType)))
			BootstrapperFactory.ContainerProvider.Register(viewType, LifetimeType.Transient);
	}

	#region Simplify.Web types registration

	/// <summary>
	/// Registers the configuration.
	/// </summary>
	public virtual void RegisterConfiguration()
	{
		var environmentName = global::System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

		var builder = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", true)
			.AddJsonFile($"appsettings.{environmentName}.json", true);

		BootstrapperFactory.ContainerProvider.Register<IConfiguration>(p => builder.Build(), LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controllers meta store.
	/// </summary>
	public virtual void RegisterControllersMetaStore() =>
		BootstrapperFactory.ContainerProvider.Register(p => ControllersMetaStore.Current, LifetimeType.Singleton);

	/// <summary>
	/// Registers the views meta store.
	/// </summary>
	public virtual void RegisterViewsMetaStore() =>
		BootstrapperFactory.ContainerProvider.Register(p => ViewsMetaStore.Current, LifetimeType.Singleton);

	/// <summary>
	/// Registers the Simplify.Web settings.
	/// </summary>
	public virtual void RegisterSimplifyWebSettings() =>
		BootstrapperFactory.ContainerProvider.Register<ISimplifyWebSettings, SimplifyWebSettings>(LifetimeType.Singleton);

	/// <summary>
	/// Registers the view factory.
	/// </summary>
	public virtual void RegisterViewFactory() =>
		BootstrapperFactory.ContainerProvider.Register<IViewFactory, ViewFactory>(LifetimeType.Singleton);

	/// <summary>
	/// Registers the controller factory.
	/// </summary>
	public virtual void RegisterControllerFactory() =>
		BootstrapperFactory.ContainerProvider.Register<IControllerFactory, ControllerFactory>(LifetimeType.Singleton);

	/// <summary>
	/// Registers the controller path parser.
	/// </summary>
	public virtual void RegisterControllerPathParser() =>
		BootstrapperFactory.ContainerProvider.Register<IControllerPathParser, ControllerPathParser>(LifetimeType.Singleton);

	/// <summary>
	/// Registers the route matcher.
	/// </summary>
	public virtual void RegisterRouteMatcher() =>
		BootstrapperFactory.ContainerProvider.Register<IRouteMatcher, RouteMatcher>(LifetimeType.Singleton);

	/// <summary>
	/// Registers the controllers agent.
	/// </summary>
	public virtual void RegisterControllersAgent() =>
		BootstrapperFactory.ContainerProvider.Register<IControllersAgent, ControllersAgent>(LifetimeType.Singleton);

	/// <summary>
	/// Registers the controller response builder.
	/// </summary>
	public virtual void RegisterControllerResponseBuilder() =>
		BootstrapperFactory.ContainerProvider.Register<IControllerResponseBuilder, ControllerResponseBuilder>(LifetimeType.Singleton);

	/// <summary>
	/// Registers the controller executor.
	/// </summary>
	public virtual void RegisterControllerExecutor() => BootstrapperFactory.ContainerProvider.Register<IControllerExecutor, ControllerExecutor>();

	/// <summary>
	/// Registers the controllers processor.
	/// </summary>
	public virtual void RegisterControllersProcessor() =>
		BootstrapperFactory.ContainerProvider.Register<IControllersProcessor, ControllersProcessor>();

	/// <summary>
	/// Registers the environment.
	/// </summary>
	public virtual void RegisterEnvironment() =>
		BootstrapperFactory.ContainerProvider.Register<IEnvironment>(
			p => new Modules.Environment(AppDomain.CurrentDomain.BaseDirectory ?? "", p.Resolve<ISimplifyWebSettings>()));

	/// <summary>
	/// Registers the language manager provider.
	/// </summary>
	public virtual void RegisterLanguageManagerProvider() =>
		BootstrapperFactory.ContainerProvider.Register<ILanguageManagerProvider>(p => new LanguageManagerProvider(p.Resolve<ISimplifyWebSettings>()));

	/// <summary>
	/// Registers the template factory.
	/// </summary>
	public virtual void RegisterTemplateFactory() =>
		BootstrapperFactory.ContainerProvider.Register<ITemplateFactory>(
			p =>
			{
				var settings = p.Resolve<ISimplifyWebSettings>();

				return new TemplateFactory(p.Resolve<IEnvironment>(), p.Resolve<ILanguageManagerProvider>(),
					settings.DefaultLanguage, settings.TemplatesMemoryCache, settings.LoadTemplatesFromAssembly);
			});

	/// <summary>
	/// Registers the file reader.
	/// </summary>
	public virtual void RegisterFileReader() =>
		BootstrapperFactory.ContainerProvider.Register<IFileReader>(
			p =>
			{
				var settings = p.Resolve<ISimplifyWebSettings>();

				return new FileReader(p.Resolve<IEnvironment>().DataPhysicalPath, p.Resolve<ISimplifyWebSettings>().DefaultLanguage,
					p.Resolve<ILanguageManagerProvider>(), settings.DisableFileReaderCache);
			});

	/// <summary>
	/// Registers the string table.
	/// </summary>
	public virtual void RegisterStringTable() =>
		BootstrapperFactory.ContainerProvider.Register<IStringTable>(
			p =>
			{
				var settings = p.Resolve<ISimplifyWebSettings>();
				return new StringTable(settings.StringTableFiles, settings.DefaultLanguage, p.Resolve<ILanguageManagerProvider>(),
					p.Resolve<IFileReader>(), settings.StringTableMemoryCache);
			});

	/// <summary>
	/// Registers the data collector.
	/// </summary>
	public virtual void RegisterDataCollector() =>
		BootstrapperFactory.ContainerProvider.Register<IDataCollector>(p =>
		{
			var settings = p.Resolve<ISimplifyWebSettings>();

			return new DataCollector(settings.DefaultMainContentVariableName, settings.DefaultTitleVariableName, p.Resolve<IStringTable>());
		});

	/// <summary>
	/// Registers the lists generator.
	/// </summary>
	public virtual void RegisterListsGenerator() => BootstrapperFactory.ContainerProvider.Register<IListsGenerator, ListsGenerator>();

	/// <summary>
	/// Registers the string table items setter.
	/// </summary>
	public virtual void RegisterStringTableItemsSetter() =>
		BootstrapperFactory.ContainerProvider.Register<IStringTableItemsSetter, StringTableItemsSetter>();

	/// <summary>
	/// Registers the page builder.
	/// </summary>
	public virtual void RegisterPageBuilder() => BootstrapperFactory.ContainerProvider.Register<IPageBuilder, PageBuilder>();

	/// <summary>
	/// Registers the response writer.
	/// </summary>
	public virtual void RegisterResponseWriter() =>
		BootstrapperFactory.ContainerProvider.Register<IResponseWriter, ResponseWriter>(LifetimeType.Singleton);

	/// <summary>
	/// Registers the page processor.
	/// </summary>
	public virtual void RegisterPageProcessor() => BootstrapperFactory.ContainerProvider.Register<IPageProcessor, PageProcessor>();

	/// <summary>
	/// Registers the controllers request handler.
	/// </summary>
	public virtual void RegisterControllersRequestHandler() =>
		BootstrapperFactory.ContainerProvider.Register<IControllersRequestHandler, ControllersRequestHandler>();

	/// <summary>
	/// Registers the static file response factory
	/// </summary>
	public virtual void RegisterStaticFileResponseFactory() =>
		BootstrapperFactory.ContainerProvider.Register<IStaticFileResponseFactory, StaticFileResponseFactory>(LifetimeType.Singleton);

	/// <summary>
	/// Registers the static file handler.
	/// </summary>
	public virtual void RegisterStaticFileHandler() =>
		BootstrapperFactory.ContainerProvider.Register<IStaticFileHandler>(
			p =>
				new StaticFileHandler(p.Resolve<ISimplifyWebSettings>().StaticFilesPaths,
					p.Resolve<IEnvironment>().SitePhysicalPath));

	/// <summary>
	/// Registers the static files request handler.
	/// </summary>
	public virtual void RegisterStaticFilesRequestHandler() =>
		BootstrapperFactory.ContainerProvider.Register<IStaticFilesRequestHandler, StaticFilesRequestHandler>();

	/// <summary>
	/// Registers the request handler.
	/// </summary>
	public virtual void RegisterRequestHandler() =>
		BootstrapperFactory.ContainerProvider.Register<IRequestHandler>(
			p =>
				new RequestHandler(p.Resolve<IControllersRequestHandler>(),
					p.Resolve<IStaticFilesRequestHandler>(), p.Resolve<ISimplifyWebSettings>().StaticFilesEnabled));

	/// <summary>
	/// Registers the stopwatch provider.
	/// </summary>
	public virtual void RegisterStopwatchProvider() =>
		BootstrapperFactory.ContainerProvider.Register<IStopwatchProvider, StopwatchProvider>();

	/// <summary>
	/// Registers the context variables setter.
	/// </summary>
	public virtual void RegisterContextVariablesSetter() =>
		BootstrapperFactory.ContainerProvider.Register<IContextVariablesSetter>(
			p =>
				new ContextVariablesSetter(p.Resolve<IDataCollector>(), p.Resolve<ISimplifyWebSettings>().DisableAutomaticSiteTitleSet));

	/// <summary>
	/// Registers the web context provider.
	/// </summary>
	public virtual void RegisterWebContextProvider() =>
		BootstrapperFactory.ContainerProvider.Register<IWebContextProvider, WebContextProvider>();

	/// <summary>
	/// Registers the redirector.
	/// </summary>
	public virtual void RegisterRedirector() =>
		BootstrapperFactory.ContainerProvider.Register<IRedirector>(p => new Redirector(p.Resolve<IWebContextProvider>().Get()));

	/// <summary>
	/// Registers the model handler.
	/// </summary>
	public virtual void RegisterModelHandler() =>
		BootstrapperFactory.ContainerProvider.Register<IModelHandler>(p => new HttpModelHandler(p.Resolve<IWebContextProvider>().Get()));

	/// <summary>
	/// Registers the default model binders.
	/// </summary>
	public virtual void RegisterDefaultModelBinders()
	{
		BootstrapperFactory.ContainerProvider.Register<HttpQueryModelBinder>(LifetimeType.Singleton);
		BootstrapperFactory.ContainerProvider.Register<HttpFormModelBinder>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the default model validators.
	/// </summary>
	public virtual void RegisterDefaultModelValidators() =>
		BootstrapperFactory.ContainerProvider.Register(r => new ValidationAttributesExecutor(), LifetimeType.Singleton);

	#endregion Simplify.Web types registration
}