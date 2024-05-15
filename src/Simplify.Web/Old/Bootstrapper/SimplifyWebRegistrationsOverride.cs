using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Simplify.DI;
using Simplify.Web.Old.Core;
using Simplify.Web.Old.Core.Controllers.Execution;
using Simplify.Web.Old.Core.Views;
using Simplify.Web.Old.Model;
using Simplify.Web.Old.Modules;
using Simplify.Web.Old.Modules.Data;
using Simplify.Web.Old.Modules.Data.Html;
using Simplify.Web.Old.Routing;

namespace Simplify.Web.Old.Bootstrapper;

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
	/// Overrides the `IControllerResponseBuilder` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideControllerResponseBuilder(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IControllerResponseBuilder), registrator);

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
	/// Overrides the `IResponseWriter` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideResponseWriter(Action<IDIRegistrator> registrator)
	{
		_actions.Add(typeof(IResponseWriter), registrator);

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