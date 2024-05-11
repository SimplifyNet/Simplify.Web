using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.DI;

namespace Simplify.Web.Bootstrapper.SimplifyWebRegistrationsOverride;

/// <summary>
/// Provides the Simplify.Web types registrations override mechanism.
/// </summary>
public partial class RegistrationsOverride
{
	private readonly IDictionary<Type, Action<IDIRegistrator>> _actions = new Dictionary<Type, Action<IDIRegistrator>>();

	/// <summary>
	/// Gets the types to exclude from registrations
	/// </summary>
	public IEnumerable<Type> GetTypesToExclude() => _actions.Select(x => x.Key);

	/// <summary>
	/// Registers the overridden types in IOC registrator
	/// </summary>
	/// <param name="registrator">IOC Container registrator.</param>
	public void RegisterActions(IDIRegistrator registrator)
	{
		foreach (var item in _actions)
			item.Value.Invoke(registrator);
	}

	/// <summary>
	/// Adds the custom registration action for specified type.
	/// </summary>
	/// <typeparam name="T">The specified type</typeparam>
	/// <param name="action">The custom registration action.</param>
	private RegistrationsOverride AddAction<T>(Action<IDIRegistrator> action) => AddAction(typeof(T), action);

	/// <summary>
	/// Adds the custom registration action for specified type.
	/// </summary>
	/// <param name="type">The specified type.</param>
	/// <param name="action">The custom registration action.</param>
	private RegistrationsOverride AddAction(Type type, Action<IDIRegistrator> action)
	{
		_actions.Add(type, action);

		return this;
	}
}