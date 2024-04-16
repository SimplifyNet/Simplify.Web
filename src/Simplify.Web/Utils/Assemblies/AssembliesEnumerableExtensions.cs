using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Simplify.Web.Utils.Assemblies;

/// <summary>
/// Provides the Assembly enumerable extensions
/// </summary>
public static class AssembliesEnumerableExtensions
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
		.Where(assembly => !excludedAssembliesPrefixes
		.Any(prefix => assembly.FullName!.StartsWith(prefix)));
}