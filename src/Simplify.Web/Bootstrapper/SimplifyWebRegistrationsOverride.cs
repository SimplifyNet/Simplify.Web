using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Simplify.DI;
using Simplify.Web.Core.Controllers.Execution;

namespace Simplify.Web.Bootstrapper
{
	/// <summary>
	/// Provides Simplify.Web types registrations override mechanism
	/// </summary>
	public class SimplifyWebRegistrationsOverride
	{
		private IDictionary<Type, Action<IDIRegistrator>> _actions = new Dictionary<Type, Action<IDIRegistrator>>();

		/// <summary>
		/// Overrides the `IConfiguration` registration
		/// </summary>
		/// <param name="registrator">IOC Container registrator</param>
		public SimplifyWebRegistrationsOverride OverrideConfiguration(Action<IDIRegistrator> registrator)
		{
			_actions.Add(typeof(IConfiguration), registrator);

			return this;
		}

		/// <summary>
		/// Overrides the `IControllerExecutor` registrations
		/// </summary>
		/// <param name="registrator">IOC container registrator</param>
		public SimplifyWebRegistrationsOverride OverrideControllerExecutor(Action<IDIRegistrator> registrator)
		{
			_actions.Add(typeof(IControllerExecutor), registrator);

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
}