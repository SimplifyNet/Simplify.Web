using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Simplify.DI;
using Simplify.Web.Core;
using Simplify.Web.Core.Controllers;
using Simplify.Web.Core.Controllers.Execution;
using Simplify.Web.Core.Controllers.Execution.V1;
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
	/// Provides `Simplify.Web` types to exclude from registrations
	/// </summary>
	protected IEnumerable<Type> TypesToExclude { get; private set; } = new List<Type>();

	/// <summary>
	/// Registers the types in container.
	/// </summary>
	// ReSharper disable once MethodTooLong

	public void Register(IEnumerable<Type>? typesToExclude = null)
	{
		if (typesToExclude != null)
			TypesToExclude = typesToExclude;

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
		if (TypesToExclude.Contains(typeof(IConfiguration)))
			return;

		var environmentName = global::System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

		var builder = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", true)
			.AddJsonFile($"appsettings.{environmentName}.json", true);

		BootstrapperFactory.ContainerProvider.Register<IConfiguration>(p => builder.Build(), LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controllers meta store.
	/// </summary>
	public virtual void RegisterControllersMetaStore()
	{
		if (TypesToExclude.Contains(typeof(IControllersMetaStore)))
			return;

		BootstrapperFactory.ContainerProvider.Register(p => ControllersMetaStore.Current, LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the views meta store.
	/// </summary>
	public virtual void RegisterViewsMetaStore()
	{
		if (TypesToExclude.Contains(typeof(IViewsMetaStore)))
			return;

		BootstrapperFactory.ContainerProvider.Register(p => ViewsMetaStore.Current, LifetimeType.Singleton);
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

	/// <summary>
	/// Registers the view factory.
	/// </summary>
	public virtual void RegisterViewFactory()
	{
		if (TypesToExclude.Contains(typeof(IViewFactory)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IViewFactory, ViewFactory>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controller factory.
	/// </summary>
	public virtual void RegisterControllerFactory()
	{
		if (TypesToExclude.Contains(typeof(IControllerFactory)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IControllerFactory, ControllerFactory>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controller path parser.
	/// </summary>
	public virtual void RegisterControllerPathParser()
	{
		if (TypesToExclude.Contains(typeof(IControllerPathParser)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IControllerPathParser, ControllerPathParser>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the route matcher.
	/// </summary>
	public virtual void RegisterRouteMatcher()
	{
		if (TypesToExclude.Contains(typeof(IRouteMatcher)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IRouteMatcher, RouteMatcher>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controllers agent.
	/// </summary>
	public virtual void RegisterControllersAgent()
	{
		if (TypesToExclude.Contains(typeof(IControllersAgent)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IControllersAgent, ControllersAgent>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controller response builder.
	/// </summary>
	public virtual void RegisterControllerResponseBuilder()
	{
		if (TypesToExclude.Contains(typeof(IControllerResponseBuilder)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IControllerResponseBuilder, ControllerResponseBuilder>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controller executor.
	/// </summary>
	public virtual void RegisterControllerExecutor()
	{
		if (TypesToExclude.Contains(typeof(IControllerExecutor)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IControllerExecutor, ControllerExecutor>();
	}

	/// <summary>
	/// Registers the controllers processor.
	/// </summary>
	public virtual void RegisterControllersProcessor()
	{
		if (TypesToExclude.Contains(typeof(IControllersProcessor)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IControllersProcessor, ControllersProcessor>();
	}

	/// <summary>
	/// Registers the environment.
	/// </summary>
	public virtual void RegisterEnvironment()
	{
		if (TypesToExclude.Contains(typeof(IEnvironment)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IEnvironment>(
			p => new Modules.Environment(AppDomain.CurrentDomain.BaseDirectory ?? "", p.Resolve<ISimplifyWebSettings>()));
	}

	/// <summary>
	/// Registers the language manager provider.
	/// </summary>
	public virtual void RegisterLanguageManagerProvider()
	{
		if (TypesToExclude.Contains(typeof(ILanguageManagerProvider)))
			return;

		BootstrapperFactory.ContainerProvider.Register<ILanguageManagerProvider>(p => new LanguageManagerProvider(p.Resolve<ISimplifyWebSettings>()));
	}

	/// <summary>
	/// Registers the template factory.
	/// </summary>
	public virtual void RegisterTemplateFactory()
	{
		if (TypesToExclude.Contains(typeof(ITemplateFactory)))
			return;

		BootstrapperFactory.ContainerProvider.Register<ITemplateFactory>(
			p =>
			{
				var settings = p.Resolve<ISimplifyWebSettings>();

				return new TemplateFactory(p.Resolve<IEnvironment>(), p.Resolve<ILanguageManagerProvider>(),
					settings.DefaultLanguage, settings.TemplatesMemoryCache, settings.LoadTemplatesFromAssembly);
			});
	}

	/// <summary>
	/// Registers the file reader.
	/// </summary>
	public virtual void RegisterFileReader()
	{
		if (TypesToExclude.Contains(typeof(IFileReader)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IFileReader>(
			p =>
			{
				var settings = p.Resolve<ISimplifyWebSettings>();

				return new FileReader(p.Resolve<IEnvironment>().DataPhysicalPath, p.Resolve<ISimplifyWebSettings>().DefaultLanguage,
					p.Resolve<ILanguageManagerProvider>(), settings.DisableFileReaderCache);
			});
	}

	/// <summary>
	/// Registers the string table.
	/// </summary>
	public virtual void RegisterStringTable()
	{
		if (TypesToExclude.Contains(typeof(IStringTable)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IStringTable>(
			p =>
			{
				var settings = p.Resolve<ISimplifyWebSettings>();
				return new StringTable(settings.StringTableFiles, settings.DefaultLanguage, p.Resolve<ILanguageManagerProvider>(),
					p.Resolve<IFileReader>(), settings.StringTableMemoryCache);
			});
	}

	/// <summary>
	/// Registers the data collector.
	/// </summary>
	public virtual void RegisterDataCollector()
	{
		if (TypesToExclude.Contains(typeof(IDataCollector)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IDataCollector>(p =>
		{
			var settings = p.Resolve<ISimplifyWebSettings>();

			return new DataCollector(settings.DefaultMainContentVariableName, settings.DefaultTitleVariableName, p.Resolve<IStringTable>());
		});
	}

	/// <summary>
	/// Registers the lists generator.
	/// </summary>
	public virtual void RegisterListsGenerator()
	{
		if (TypesToExclude.Contains(typeof(IListsGenerator)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IListsGenerator, ListsGenerator>();
	}

	/// <summary>
	/// Registers the string table items setter.
	/// </summary>
	public virtual void RegisterStringTableItemsSetter()
	{
		if (TypesToExclude.Contains(typeof(IStringTableItemsSetter)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IStringTableItemsSetter, StringTableItemsSetter>();
	}

	/// <summary>
	/// Registers the page builder.
	/// </summary>
	public virtual void RegisterPageBuilder()
	{
		if (TypesToExclude.Contains(typeof(IPageBuilder)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IPageBuilder, PageBuilder>();
	}

	/// <summary>
	/// Registers the response writer.
	/// </summary>
	public virtual void RegisterResponseWriter()
	{
		if (TypesToExclude.Contains(typeof(IResponseWriter)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IResponseWriter, ResponseWriter>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the page processor.
	/// </summary>
	public virtual void RegisterPageProcessor()
	{
		if (TypesToExclude.Contains(typeof(IPageProcessor)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IPageProcessor, PageProcessor>();
	}

	/// <summary>
	/// Registers the controllers request handler.
	/// </summary>
	public virtual void RegisterControllersRequestHandler()
	{
		if (TypesToExclude.Contains(typeof(IControllersRequestHandler)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IControllersRequestHandler, ControllersRequestHandler>();
	}

	/// <summary>
	/// Registers the static file response factory
	/// </summary>
	public virtual void RegisterStaticFileResponseFactory()
	{
		if (TypesToExclude.Contains(typeof(IStaticFileResponseFactory)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IStaticFileResponseFactory, StaticFileResponseFactory>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the static file handler.
	/// </summary>
	public virtual void RegisterStaticFileHandler()
	{
		if (TypesToExclude.Contains(typeof(IStaticFileHandler)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IStaticFileHandler>(
			p =>
				new StaticFileHandler(p.Resolve<ISimplifyWebSettings>().StaticFilesPaths,
					p.Resolve<IEnvironment>().SitePhysicalPath));
	}

	/// <summary>
	/// Registers the static files request handler.
	/// </summary>
	public virtual void RegisterStaticFilesRequestHandler()
	{
		if (TypesToExclude.Contains(typeof(IStaticFilesRequestHandler)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IStaticFilesRequestHandler, StaticFilesRequestHandler>();
	}

	/// <summary>
	/// Registers the request handler.
	/// </summary>
	public virtual void RegisterRequestHandler()
	{
		if (TypesToExclude.Contains(typeof(IRequestHandler)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IRequestHandler>(
				p =>
					new RequestHandler(p.Resolve<IControllersRequestHandler>(),
						p.Resolve<IStaticFilesRequestHandler>(), p.Resolve<ISimplifyWebSettings>().StaticFilesEnabled));
	}

	/// <summary>
	/// Registers the stopwatch provider.
	/// </summary>
	public virtual void RegisterStopwatchProvider()
	{
		if (TypesToExclude.Contains(typeof(IStopwatchProvider)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IStopwatchProvider, StopwatchProvider>();
	}

	/// <summary>
	/// Registers the context variables setter.
	/// </summary>
	public virtual void RegisterContextVariablesSetter()
	{
		if (TypesToExclude.Contains(typeof(IContextVariablesSetter)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IContextVariablesSetter>(
				p =>
					new ContextVariablesSetter(p.Resolve<IDataCollector>(), p.Resolve<ISimplifyWebSettings>().DisableAutomaticSiteTitleSet));
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

	/// <summary>
	/// Registers the redirector.
	/// </summary>
	public virtual void RegisterRedirector()
	{
		if (TypesToExclude.Contains(typeof(IRedirector)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IRedirector>(p => new Redirector(p.Resolve<IWebContextProvider>().Get()));
	}

	/// <summary>
	/// Registers the model handler.
	/// </summary>
	public virtual void RegisterModelHandler()
	{
		if (TypesToExclude.Contains(typeof(IModelHandler)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IModelHandler>(p => new HttpModelHandler(p.Resolve<IWebContextProvider>().Get()));
	}

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