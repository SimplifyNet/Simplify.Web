using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Simplify.DI;
using Simplify.Web.Settings;

namespace Simplify.Web.Bootstrapper;

/// <summary>
/// Provides the Simplify.Web types registrations override mechanism.
/// </summary>
public class SimplifyWebRegistrationsOverride
{
	private readonly IDictionary<Type, Action<IDIRegistrator>> _actions = new Dictionary<Type, Action<IDIRegistrator>>();

	/// <summary>
	/// Gets the types to exclude from registrations
	/// </summary>
	public IEnumerable<Type> GetTypesToExclude() => _actions.Select(x => x.Key);

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
	/// Overrides the `IConfiguration` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideConfiguration(Action<IDIRegistrator> registrator) => AddAction(typeof(IConfiguration), registrator);

	/// <summary>
	/// Overrides the `ISimplifyWebSettings` registration.
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public SimplifyWebRegistrationsOverride OverrideSimplifyWebSettings(Action<IDIRegistrator> registrator) => AddAction(typeof(ISimplifyWebSettings), registrator);

	private SimplifyWebRegistrationsOverride AddAction(Type type, Action<IDIRegistrator> action)
	{
		_actions.Add(type, action);

		return this;
	}
}