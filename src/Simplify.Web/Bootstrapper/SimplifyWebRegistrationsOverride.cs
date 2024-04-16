using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.DI;

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
}