using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Simplify.DI;
using Simplify.Web.Core;
using Simplify.Web.Core.Controllers;
using Simplify.Web.Core.Controllers.Execution;
using Simplify.Web.Core.PageAssembly;
using Simplify.Web.Core.StaticFiles;
using Simplify.Web.Core.Views;
using Simplify.Web.Diagnostics.Measurement;
using Simplify.Web.Meta;
using Simplify.Web.Model;
using Simplify.Web.Modules;
using Simplify.Web.Modules.Data;
using Simplify.Web.Modules.Data.Html;
using Simplify.Web.Routing;
using Simplify.Web.Settings;

namespace Simplify.Web.Bootstrapper;

/// <summary>
/// Provides Simplify.Web types registrations override mechanism.
/// </summary>
public class SimplifyWebRegistrationsOverride
{
	private readonly IDictionary<Type, Action<IDIRegistrator>> _actions = new Dictionary<Type, Action<IDIRegistrator>>();

	/// <summary>
	/// Overrides the `IConfiguration` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideConfiguration(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IConfiguration), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IControllersMetaStore` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideControllersMetaStore(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IControllersMetaStore), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IViewsMetaStore` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideViewsMetaStore(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IViewsMetaStore), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `ISimplifyWebSettings` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideSimplifyWebSettings(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(ISimplifyWebSettings), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IViewFactory` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideViewFactory(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IViewFactory), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IController1Factory` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideController1Factory(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IController1Factory), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IController2Factory` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideController2Factory(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IController2Factory), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IControllerPathParser` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideControllerPathParser(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IControllerPathParser), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IRouteMatcher` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideRouteMatcher(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IRouteMatcher), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IControllersAgent` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideControllersAgent(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IControllersAgent), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IControllerResponseBuilder` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideControllerResponseBuilder(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IControllerResponseBuilder), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `Controller1Executor` registrations
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideController1Executor(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(Controller1Executor), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `Controller2Executor` registrations
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideController2Executor(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(Controller2Executor), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IControllerExecutor` registrations
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideVersionedControllerExecutorsList(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IList<IVersionedControllerExecutor>), registrator);

		return this;
	}


	/// <summary>
	/// Overrides the `IControllerExecutor` registrations
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideControllerExecutor(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IControllerExecutor), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IControllersProcessor` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideControllersProcessor(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IControllersProcessor), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IEnvironment` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideEnvironment(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IEnvironment), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `ILanguageManagerProvider` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideLanguageManagerProvider(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(ILanguageManagerProvider), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `ITemplateFactory` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideTemplateFactory(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(ITemplateFactory), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IFileReader` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideFileReader(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IFileReader), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IStringTable` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideStringTable(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IStringTable), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IDataCollector` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideDataCollector(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IDataCollector), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IListsGenerator` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideListsGenerator(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IListsGenerator), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IStringTableItemsSetter` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideStringTableItemsSetter(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IStringTableItemsSetter), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IPageBuilder` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverridePageBuilder(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IPageBuilder), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IResponseWriter` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideResponseWriter(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IResponseWriter), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IPageProcessor` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverridePageProcessor(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IPageProcessor), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IControllersRequestHandler` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideControllersRequestHandler(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IControllersRequestHandler), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IStaticFileResponseFactory` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideStaticFileResponseFactory(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IStaticFileResponseFactory), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IStaticFileHandler` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideStaticFileHandler(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IStaticFileHandler), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IStaticFilesRequestHandler` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideStaticFilesRequestHandler(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IStaticFilesRequestHandler), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IRequestHandler` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideRequestHandler(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IRequestHandler), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IStopwatchProvider` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideStopwatchProvider(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IStopwatchProvider), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IContextVariablesSetter` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideContextVariablesSetter(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IContextVariablesSetter), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IWebContextProvider` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideWebContextProvider(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IWebContextProvider), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IRedirector` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideRedirector(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IRedirector), registrator);

		return this;
	}

	/// <summary>
	/// Overrides the `IModelHandler` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideModelHandler(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IModelHandler), registrator);

		return this;
	}

	/// <summary>
	/// Registers the overridden types in IOC registrator
	/// </summary>
	/// <param name="registrator"></param>
	public void RegisterActions(IDIRegistrator registrator)
	{
		foreach (var item in _actions)
			item.Value.Invoke(registrator);
	}

	/// <summary>
	/// Gets the types to exclude from registrations
	/// </summary>
	public IEnumerable<Type> GetTypesToExclude() => _actions.Select(x => x.Key);
}