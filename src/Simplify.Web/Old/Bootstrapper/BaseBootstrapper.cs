using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Simplify.DI;
using Simplify.Web.Old.Core;
using Simplify.Web.Old.Core.Controllers;
using Simplify.Web.Old.Core.Controllers.Execution;
using Simplify.Web.Old.Core.PageAssembly;
using Simplify.Web.Old.Core.StaticFiles;
using Simplify.Web.Old.Core.Views;
using Simplify.Web.Old.Diagnostics.Measurement;
using Simplify.Web.Old.Meta;
using Simplify.Web.Old.Model;
using Simplify.Web.Old.Model.Binding;
using Simplify.Web.Old.Model.Validation;
using Simplify.Web.Old.Modules;
using Simplify.Web.Old.Modules.Data;
using Simplify.Web.Old.Modules.Data.Html;
using Simplify.Web.Old.Routing;
using Simplify.Web.Old.Settings;

namespace Simplify.Web.Old.Bootstrapper;

/// <summary>
/// Base and default Simplify.Web bootstrapper.
/// </summary>
// ReSharper disable once ClassTooBig
public class BaseBootstrapper
{
	/// <summary>
	/// Provides `Simplify.Web` types to exclude from registrations.
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

		RegisterSimplifyWebSettings();
		RegisterViewFactory();
		RegisterController1Factory();
		RegisterController2Factory();
		RegisterControllerPathParser();
		RegisterRouteMatcher();
		RegisterControllersAgent();
		RegisterControllerResponseBuilder();
		RegisterController1Executor();
		RegisterController2Executor();
		RegisterVersionedControllerExecutorsList();
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

		BootstrapperFactory.ContainerProvider.Register<IConfiguration>(r => builder.Build(), LifetimeType.Singleton);
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
	/// Registers the controller v1 factory.
	/// </summary>
	public virtual void RegisterController1Factory()
	{
		if (TypesToExclude.Contains(typeof(IController1Factory)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IController1Factory, Controller1Factory>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controller v2 factory.
	/// </summary>
	public virtual void RegisterController2Factory()
	{
		if (TypesToExclude.Contains(typeof(IController2Factory)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IController2Factory, Controller2Factory>(LifetimeType.Singleton);
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
	/// Registers the controller v1 executor.
	/// </summary>
	public virtual void RegisterController1Executor()
	{
		if (TypesToExclude.Contains(typeof(Controller1Executor)))
			return;

		BootstrapperFactory.ContainerProvider.Register<Controller1Executor>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controller v2 executor.
	/// </summary>
	public virtual void RegisterController2Executor()
	{
		if (TypesToExclude.Contains(typeof(Controller2Executor)))
			return;

		BootstrapperFactory.ContainerProvider.Register<Controller2Executor>(LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the versioned controller executors list.
	/// </summary>
	public virtual void RegisterVersionedControllerExecutorsList()
	{
		if (TypesToExclude.Contains(typeof(IList<IVersionedControllerExecutor>)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IList<IVersionedControllerExecutor>>(r => new List<IVersionedControllerExecutor>
		{
			r.Resolve<Controller1Executor>(),
			r.Resolve<Controller2Executor>()
		}, LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controller executor.
	/// </summary>
	public virtual void RegisterControllerExecutor()
	{
		if (TypesToExclude.Contains(typeof(IControllerExecutor)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IControllerExecutor, ControllerExecutor>(LifetimeType.Singleton);
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

		BootstrapperFactory.ContainerProvider.Register<IEnvironment>(r =>
			new Modules.Environment(AppDomain.CurrentDomain.BaseDirectory ?? "", r.Resolve<ISimplifyWebSettings>()));
	}

	/// <summary>
	/// Registers the language manager provider.
	/// </summary>
	public virtual void RegisterLanguageManagerProvider()
	{
		if (TypesToExclude.Contains(typeof(ILanguageManagerProvider)))
			return;

		BootstrapperFactory.ContainerProvider.Register<ILanguageManagerProvider>(r => new LanguageManagerProvider(r.Resolve<ISimplifyWebSettings>()));
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
	/// Registers the file reader.
	/// </summary>
	public virtual void RegisterFileReader()
	{
		if (TypesToExclude.Contains(typeof(IFileReader)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IFileReader>(r =>
		{
			var settings = r.Resolve<ISimplifyWebSettings>();

			return new FileReader(
				r.Resolve<IEnvironment>().DataPhysicalPath,
				r.Resolve<ISimplifyWebSettings>().DefaultLanguage,
				r.Resolve<ILanguageManagerProvider>(),
				settings.DisableFileReaderCache);
		});
	}

	/// <summary>
	/// Registers the string table.
	/// </summary>
	public virtual void RegisterStringTable()
	{
		if (TypesToExclude.Contains(typeof(IStringTable)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IStringTable>(r =>
		{
			var settings = r.Resolve<ISimplifyWebSettings>();

			return new StringTable(
				settings.StringTableFiles,
				settings.DefaultLanguage,
				r.Resolve<ILanguageManagerProvider>(),
				r.Resolve<IFileReader>(),
				settings.StringTableMemoryCache);
		});
	}

	/// <summary>
	/// Registers the data collector.
	/// </summary>
	public virtual void RegisterDataCollector()
	{
		if (TypesToExclude.Contains(typeof(IDataCollector)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IDataCollector>(r =>
		{
			var settings = r.Resolve<ISimplifyWebSettings>();

			return new DataCollector(
				settings.DefaultMainContentVariableName,
				settings.DefaultTitleVariableName,
				r.Resolve<IStringTable>());
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

		BootstrapperFactory.ContainerProvider.Register<IStaticFileHandler>(r =>
			new StaticFileHandler(
				r.Resolve<ISimplifyWebSettings>().StaticFilesPaths,
				r.Resolve<IEnvironment>().SitePhysicalPath));
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

		BootstrapperFactory.ContainerProvider.Register<IRequestHandler>(r =>
			new RequestHandler(
				r.Resolve<IControllersRequestHandler>(),
				r.Resolve<IStaticFilesRequestHandler>(),
				r.Resolve<ISimplifyWebSettings>().StaticFilesEnabled));
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

		BootstrapperFactory.ContainerProvider.Register<IContextVariablesSetter>(r =>
			new ContextVariablesSetter(
				r.Resolve<IDataCollector>(),
				r.Resolve<ISimplifyWebSettings>().DisableAutomaticSiteTitleSet));
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

		BootstrapperFactory.ContainerProvider.Register<IRedirector>(r => new Redirector(r.Resolve<IWebContextProvider>().Get()));
	}

	/// <summary>
	/// Registers the model handler.
	/// </summary>
	public virtual void RegisterModelHandler()
	{
		if (TypesToExclude.Contains(typeof(IModelHandler)))
			return;

		BootstrapperFactory.ContainerProvider.Register<IModelHandler>(r => new HttpModelHandler(r.Resolve<IWebContextProvider>().Get()));
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