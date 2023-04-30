using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.DI;
using Simplify.Web.Attributes.Setup;
using Simplify.Web.Core;
using Simplify.Web.Core.Controllers;
using Simplify.Web.Core.Controllers.Execution;
using Simplify.Web.Core.Controllers.Execution.Building;
using Simplify.Web.Core.PageAssembly;
using Simplify.Web.Core.StaticFiles;
using Simplify.Web.Core.Views;
using Simplify.Web.Meta;
using Simplify.Web.ModelBinding;
using Simplify.Web.Modules;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Data.Html;
using Simplify.Web.Routing;
using Simplify.Web.Settings;
using Environment = Simplify.Web.Modules.Environment;

#nullable disable

namespace Simplify.Web.Bootstrapper;

/// <summary>
/// Base and default Simplify.Web bootstrapper
/// </summary>
public class BaseBootstrapper
{
	private Type _simplifyWebSettingsType;
	private Type _viewFactoryType;
	private Type _controllerFactoryType;
	private Type _controllerPathParserType;
	private Type _routeMatcherType;
	private Type _controllersAgentType;
	private Type _controllerResponseBuilderType;
	private Type _controllerExecutorType;
	private Type _controllersProcessorType;
	private Type _listsGeneratorType;
	private Type _stringTableItemsSetterType;
	private Type _pageBuilderType;
	private Type _responseWriterType;
	private Type _pageProcessorType;
	private Type _controllersRequestHandlerType;
	private Type _staticFileResponseFactoryType;
	private Type _staticFilesRequestHandlerType;
	private Type _requestHandlerType;
	private Type _stopwatchProviderType;
	private Type _webContextProviderType;

	/// <summary>
	/// Registers the types in container.
	/// </summary>
	public void Register()
	{
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
		RegisterModelHander();

		var ignoredTypes = GetIgnoredTypes();

		// Registering controllers types
		foreach (var controllerMetaData in ControllersMetaStore.Current.ControllersMetaData
			         .Where(controllerMetaData => ignoredTypes.All(x => x != controllerMetaData.ControllerType)))
		{
			DIContainer.Current.Register(controllerMetaData.ControllerType, LifetimeType.Transient);
		}

		// Registering views types
		foreach (var viewType in ViewsMetaStore.Current.ViewsTypes.Where(viewType => ignoredTypes.All(x => x != viewType)))
			DIContainer.Current.Register(viewType, LifetimeType.Transient);
	}

	private static IEnumerable<Type> GetIgnoredTypes()
	{
		var typesToIgnore = new List<Type>();

		var ignoreContainingClass = SimplifyWebTypesFinder.GetAllTypes().FirstOrDefault(t => t.IsDefined(typeof(IgnoreTypesRegistrationAttribute), true));

		if (ignoreContainingClass == null)
			return typesToIgnore;

		var attributes = ignoreContainingClass.GetCustomAttributes(typeof(IgnoreTypesRegistrationAttribute), false);

		typesToIgnore.AddRange(((IgnoreTypesRegistrationAttribute)attributes[0]).Types);

		return typesToIgnore;
	}

	#region Bootstrapper types

	/// <summary>
	/// Gets the type of the Simplify.Web settings.
	/// </summary>
	/// <value>
	/// The type of the Simplify.Web settings.
	/// </value>
	public Type SimplifyWebSettingsType => _simplifyWebSettingsType ?? typeof(SimplifyWebSettings);

	/// <summary>
	/// Gets the type of the view factory.
	/// </summary>
	/// <value>
	/// The type of the view factory.
	/// </value>
	public Type ViewFactoryType => _viewFactoryType ?? typeof(ViewFactory);

	/// <summary>
	/// Gets the type of the controller factory.
	/// </summary>
	/// <value>
	/// The type of the controller factory.
	/// </value>
	public Type ControllerFactoryType => _controllerFactoryType ?? typeof(ControllerFactory);

	/// <summary>
	/// Gets the controller path parser.
	/// </summary>
	/// <value>
	/// The controller path parser.
	/// </value>
	public Type ControllerPathParser => _controllerPathParserType ?? typeof(ControllerPathParser);

	/// <summary>
	/// Gets the type of the route matcher.
	/// </summary>
	/// <value>
	/// The type of the route matcher.
	/// </value>
	public Type RouteMatcherType => _routeMatcherType ?? typeof(RouteMatcher);

	/// <summary>
	/// Gets the type of the controllers agent.
	/// </summary>
	/// <value>
	/// The type of the controllers agent.
	/// </value>
	public Type ControllersAgentType => _controllersAgentType ?? typeof(ControllersAgent);

	/// <summary>
	/// Gets the type of the controller response builder.
	/// </summary>
	/// <value>
	/// The type of the controller response builder.
	/// </value>
	public Type ControllerResponseBuilderType => _controllerResponseBuilderType ?? typeof(ControllerResponseBuilder);

	/// <summary>
	/// Gets the type of the controller executor.
	/// </summary>
	/// <value>
	/// The type of the controller executor.
	/// </value>
	public Type ControllerExecutorType => _controllerExecutorType ?? typeof(ControllerExecutor);

	/// <summary>
	/// Gets the type of the controllers processor.
	/// </summary>
	/// <value>
	/// The type of the controllers processor.
	/// </value>
	public Type ControllersProcessorType => _controllersProcessorType ?? typeof(ControllersProcessor);

	/// <summary>
	/// Gets the type of the lists generator.
	/// </summary>
	/// <value>
	/// The type of the lists generator.
	/// </value>
	public Type ListsGeneratorType => _listsGeneratorType ?? typeof(ListsGenerator);

	/// <summary>
	/// Gets the type of the string table items setter.
	/// </summary>
	/// <value>
	/// The type of the string table items setter.
	/// </value>
	public Type StringTableItemsSetterType => _stringTableItemsSetterType ?? typeof(StringTableItemsSetter);

	/// <summary>
	/// Gets the type of the page builder.
	/// </summary>
	/// <value>
	/// The type of the page builder.
	/// </value>
	public Type PageBuilderType => _pageBuilderType ?? typeof(PageBuilder);

	/// <summary>
	/// Gets the type of the response writer.
	/// </summary>
	/// <value>
	/// The type of the response writer.
	/// </value>
	public Type ResponseWriterType => _responseWriterType ?? typeof(ResponseWriter);

	/// <summary>
	/// Gets the type of the page processor.
	/// </summary>
	/// <value>
	/// The type of the page processor.
	/// </value>
	public Type PageProcessorType => _pageProcessorType ?? typeof(PageProcessor);

	/// <summary>
	/// Gets the type of the controllers request handler.
	/// </summary>
	/// <value>
	/// The type of the controllers request handler.
	/// </value>
	public Type ControllersRequestHandlerType => _controllersRequestHandlerType ?? typeof(ControllersRequestHandler);

	/// <summary>
	/// Gets the type of the static file response factory.
	/// </summary>
	/// <value>
	/// The type of the static file response factory.
	/// </value>
	public Type StaticFileResponseFactoryType => _staticFileResponseFactoryType ?? typeof(StaticFileResponseFactory);

	/// <summary>
	/// Gets the type of the static files request handler.
	/// </summary>
	/// <value>
	/// The type of the static files request handler.
	/// </value>
	public Type StaticFilesRequestHandlerType => _staticFilesRequestHandlerType ?? typeof(StaticFilesRequestHandler);

	/// <summary>
	/// Gets the type of the request handler.
	/// </summary>
	/// <value>
	/// The type of the request handler.
	/// </value>
	public Type RequestHandlerType => _requestHandlerType ?? typeof(RequestHandler);

	/// <summary>
	/// Gets the type of the stopwatch provider.
	/// </summary>
	/// <value>
	/// The type of the stopwatch provider.
	/// </value>
	public Type StopwatchProviderType => _stopwatchProviderType ?? typeof(StopwatchProvider);

	/// <summary>
	/// Gets the type of the web context provider.
	/// </summary>
	/// <value>
	/// The type of the web context provider.
	/// </value>
	public Type WebContextProviderType => _webContextProviderType ?? typeof(WebContextProvider);

	#endregion Bootstrapper types

	#region Bootstrapper types override

	/// <summary>
	/// Sets the type of the Simplify.Web settings.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void SetSimplifyWebSettingsType<T>()
		where T : ISimplifyWebSettings
	{
		_simplifyWebSettingsType = typeof(T);
	}

	/// <summary>
	/// Sets the type of the view factory.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void SetViewFactoryType<T>()
		where T : IViewFactory
	{
		_viewFactoryType = typeof(T);
	}

	/// <summary>
	/// Sets the type of the controller factory.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void SetControllerFactoryType<T>()
		where T : IControllerFactory
	{
		_controllerFactoryType = typeof(T);
	}

	/// <summary>
	/// Sets the type of the controller path parser.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void SetControllerPathParserType<T>()
		where T : IControllerPathParser
	{
		_controllerPathParserType = typeof(T);
	}

	/// <summary>
	/// Sets the type of the route matcher.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void SetRouteMatcherType<T>()
		where T : IRouteMatcher
	{
		_routeMatcherType = typeof(T);
	}

	/// <summary>
	/// Sets the type of the controllers agent.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void SetControllersAgentType<T>()
		where T : IControllersAgent
	{
		_controllersAgentType = typeof(T);
	}

	/// <summary>
	/// Sets the type of the controller response builder.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void SetControllerResponseBuilderType<T>()
		where T : IControllerResponseBuilder
	{
		_controllerResponseBuilderType = typeof(T);
	}

	/// <summary>
	/// Sets the type of the controller executor.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void SetControllerExecutorType<T>()
		where T : IControllerExecutor
	{
		_controllerExecutorType = typeof(T);
	}

	/// <summary>
	/// Sets the type of the controllers processor.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void SetControllersProcessorType<T>()
		where T : IControllersProcessor
	{
		_controllersProcessorType = typeof(T);
	}

	/// <summary>
	/// Sets the type of the lists generator.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void SetListsGeneratorType<T>()
		where T : IListsGenerator
	{
		_listsGeneratorType = typeof(T);
	}

	/// <summary>
	/// Sets the type of the string table items setter.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void SetStringTableItemsSetterType<T>()
		where T : IStringTableItemsSetter
	{
		_stringTableItemsSetterType = typeof(T);
	}

	/// <summary>
	/// Sets the page builder type.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void SetPageBuilderType<T>()
		where T : IPageBuilder
	{
		_pageBuilderType = typeof(T);
	}

	/// <summary>
	/// Sets the type of the response writer.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void SetResponseWriterType<T>()
		where T : IResponseWriter
	{
		_responseWriterType = typeof(T);
	}

	/// <summary>
	/// Sets the type of the page processor.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void SetPageProcessorType<T>()
		where T : IPageProcessor
	{
		_pageProcessorType = typeof(T);
	}

	/// <summary>
	/// Sets the type of the controllers request handler.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void SetControllersRequestHandlerType<T>()
		where T : IControllersRequestHandler
	{
		_controllersRequestHandlerType = typeof(T);
	}

	/// <summary>
	/// Sets the type of the static file response factory.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void SetStaticFileResponseFactoryType<T>()
		where T : IStaticFileResponseFactory
	{
		_staticFileResponseFactoryType = typeof(T);
	}

	/// <summary>
	/// Sets the type of the static files request handler.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void SetStaticFilesRequestHandlerType<T>()
		where T : IStaticFilesRequestHandler
	{
		_staticFilesRequestHandlerType = typeof(T);
	}

	/// <summary>
	/// Sets the type of the request handler.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void SetRequestHandlerType<T>()
		where T : IRequestHandler
	{
		_requestHandlerType = typeof(T);
	}

	/// <summary>
	/// Sets the type of the stopwatch provider.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void SetStopwatchProviderType<T>()
		where T : IStopwatchProvider
	{
		_stopwatchProviderType = typeof(T);
	}

	/// <summary>
	/// Sets the web context provider.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public void SetWebContextProvider<T>()
		where T : IWebContextProvider
	{
		_webContextProviderType = typeof(T);
	}

	#endregion Bootstrapper types override

	#region Bootstrapper types registration

	/// <summary>
	/// Registers the controllers meta store.
	/// </summary>
	public virtual void RegisterControllersMetaStore()
	{
		DIContainer.Current.Register(p => ControllersMetaStore.Current, LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the views meta store.
	/// </summary>
	public virtual void RegisterViewsMetaStore()
	{
		DIContainer.Current.Register(p => ViewsMetaStore.Current, LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the Simplify.Web settings.
	/// </summary>
	public virtual void RegisterSimplifyWebSettings()
	{
		DIContainer.Current.Register<ISimplifyWebSettings>(SimplifyWebSettingsType, LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the view factory.
	/// </summary>
	public virtual void RegisterViewFactory()
	{
		DIContainer.Current.Register<IViewFactory>(ViewFactoryType, LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controller factory.
	/// </summary>
	public virtual void RegisterControllerFactory()
	{
		DIContainer.Current.Register<IControllerFactory>(ControllerFactoryType, LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controller path parser.
	/// </summary>
	public virtual void RegisterControllerPathParser()
	{
		DIContainer.Current.Register<IControllerPathParser>(ControllerPathParser, LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the route matcher.
	/// </summary>
	public virtual void RegisterRouteMatcher()
	{
		DIContainer.Current.Register<IRouteMatcher>(RouteMatcherType, LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controllers agent.
	/// </summary>
	public virtual void RegisterControllersAgent()
	{
		DIContainer.Current.Register<IControllersAgent>(ControllersAgentType, LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controller response builder.
	/// </summary>
	public virtual void RegisterControllerResponseBuilder()
	{
		DIContainer.Current.Register<IControllerResponseBuilder>(ControllerResponseBuilderType, LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the controller executor.
	/// </summary>
	public virtual void RegisterControllerExecutor()
	{
		DIContainer.Current.Register<IControllerExecutor>(ControllerExecutorType);
	}

	/// <summary>
	/// Registers the controllers processor.
	/// </summary>
	public virtual void RegisterControllersProcessor()
	{
		DIContainer.Current.Register<IControllersProcessor>(ControllersProcessorType);
	}

	/// <summary>
	/// Registers the environment.
	/// </summary>
	public virtual void RegisterEnvironment()
	{
		DIContainer.Current.Register<IEnvironment>(
			p => new Environment(AppDomain.CurrentDomain.BaseDirectory, p.Resolve<ISimplifyWebSettings>()));
	}

	/// <summary>
	/// Registers the language manager provider.
	/// </summary>
	public virtual void RegisterLanguageManagerProvider()
	{
		DIContainer.Current.Register<ILanguageManagerProvider>(p => new LanguageManagerProvider(p.Resolve<ISimplifyWebSettings>()));
	}

	/// <summary>
	/// Registers the template factory.
	/// </summary>
	public virtual void RegisterTemplateFactory()
	{
		DIContainer.Current.Register<ITemplateFactory>(
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
		DIContainer.Current.Register<IFileReader>(
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
		DIContainer.Current.Register<IStringTable>(
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
		DIContainer.Current.Register<IDataCollector>(p =>
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
		DIContainer.Current.Register<IListsGenerator>(ListsGeneratorType);
	}

	/// <summary>
	/// Registers the string table items setter.
	/// </summary>
	public virtual void RegisterStringTableItemsSetter()
	{
		DIContainer.Current.Register<IStringTableItemsSetter>(StringTableItemsSetterType);
	}

	/// <summary>
	/// Registers the page builder.
	/// </summary>
	public virtual void RegisterPageBuilder()
	{
		DIContainer.Current.Register<IPageBuilder>(PageBuilderType);
	}

	/// <summary>
	/// Registers the response writer.
	/// </summary>
	public virtual void RegisterResponseWriter()
	{
		DIContainer.Current.Register<IResponseWriter>(ResponseWriterType, LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the page processor.
	/// </summary>
	public virtual void RegisterPageProcessor()
	{
		DIContainer.Current.Register<IPageProcessor>(PageProcessorType);
	}

	/// <summary>
	/// Registers the controllers request handler.
	/// </summary>
	public virtual void RegisterControllersRequestHandler()
	{
		DIContainer.Current.Register<IControllersRequestHandler>(ControllersRequestHandlerType);
	}

	/// <summary>
	/// Registers the static file response factory
	/// </summary>
	public virtual void RegisterStaticFileResponseFactory()
	{
		DIContainer.Current.Register<IStaticFileResponseFactory>(StaticFileResponseFactoryType, LifetimeType.Singleton);
	}

	/// <summary>
	/// Registers the static file handler.
	/// </summary>
	public virtual void RegisterStaticFileHandler()
	{
		DIContainer.Current.Register<IStaticFileHandler>(
			p =>
				new StaticFileHandler(p.Resolve<ISimplifyWebSettings>().StaticFilesPaths,
					p.Resolve<IEnvironment>().SitePhysicalPath));
	}

	/// <summary>
	/// Registers the static files request handler.
	/// </summary>
	public virtual void RegisterStaticFilesRequestHandler()
	{
		DIContainer.Current.Register<IStaticFilesRequestHandler>(StaticFilesRequestHandlerType);
	}

	/// <summary>
	/// Registers the request handler.
	/// </summary>
	public virtual void RegisterRequestHandler()
	{
		DIContainer.Current.Register<IRequestHandler>(RequestHandlerType);
	}

	/// <summary>
	/// Registers the stopwatch provider.
	/// </summary>
	public virtual void RegisterStopwatchProvider()
	{
		DIContainer.Current.Register<IStopwatchProvider>(StopwatchProviderType);
	}

	/// <summary>
	/// Registers the context variables setter.
	/// </summary>
	public virtual void RegisterContextVariablesSetter()
	{
		DIContainer.Current.Register<IContextVariablesSetter>(
			p =>
				new ContextVariablesSetter(p.Resolve<IDataCollector>(), p.Resolve<ISimplifyWebSettings>().DisableAutomaticSiteTitleSet));
	}

	/// <summary>
	/// Registers the web context provider.
	/// </summary>
	public virtual void RegisterWebContextProvider()
	{
		DIContainer.Current.Register<IWebContextProvider>(WebContextProviderType);
	}

	/// <summary>
	/// Registers the redirector.
	/// </summary>
	public virtual void RegisterRedirector()
	{
		DIContainer.Current.Register<IRedirector>(p => new Redirector(p.Resolve<IWebContextProvider>().Get()));
	}

	/// <summary>
	/// Registers the model hander.
	/// </summary>
	public virtual void RegisterModelHander()
	{
		DIContainer.Current.Register<IModelHandler>(p => new HttpModelHandler(p.Resolve<IWebContextProvider>().Get()));
	}

	#endregion Bootstrapper types registration
}