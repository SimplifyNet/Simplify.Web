using System;
using System.Collections.Generic;

namespace Simplify.Web.Bootstrapper;

/// <summary>
/// Provides the base and default Simplify.Web bootstrapper.
/// </summary>
public class BaseBootstrapper
{
	/// <summary>
	/// Provides the `Simplify.Web` types to exclude from registrations.
	/// </summary>
	protected IEnumerable<Type> TypesToExclude { get; private set; } = [];

	/// <summary>
	/// Registers the types in container.
	/// </summary>
	public void Register(IEnumerable<Type>? typesToExclude = null)
	{
		if (typesToExclude != null)
			TypesToExclude = typesToExclude;
	}
}