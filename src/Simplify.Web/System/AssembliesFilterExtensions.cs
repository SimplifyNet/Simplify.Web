using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Simplify.Web.System;

/// <summary>
/// Provides the <see cref="Assembly" /> filter extensions.
/// </summary>
public static class AssembliesFilterExtensions
{
	/// <summary>
	/// Gets the assemblies types.
	/// </summary>
	/// <param name="assemblies">The assemblies.</param>
	/// <param name="excludedAssembliesPrefixes">The filter prefixes.</param>
	public static IEnumerable<Type> GetAssembliesTypes(this IEnumerable<Assembly> assemblies, IEnumerable<string> excludedAssembliesPrefixes) => assemblies
		.FilterExcludedAssemblies(excludedAssembliesPrefixes)
		.Select(x => x.GetTypes())
		.SelectMany(x => x);

	/// <summary>
	/// Filters the excluded assemblies.
	/// </summary>
	/// <param name="assemblies">The assemblies.</param>
	/// <param name="excludedAssembliesPrefixes">The filter prefixes.</param>
	public static IEnumerable<Assembly> FilterExcludedAssemblies(this IEnumerable<Assembly> assemblies, IEnumerable<string> excludedAssembliesPrefixes) => assemblies
		.Where(assembly => excludedAssembliesPrefixes
		.All(prefix => !assembly.FullName!.StartsWith(prefix)));
}