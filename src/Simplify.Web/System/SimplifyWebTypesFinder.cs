using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Simplify.Web.Attributes.Setup;

namespace Simplify.Web.System;

/// <summary>
/// Provides the Simplify.Web types finder among all loaded assemblies.
/// </summary>
public static class SimplifyWebTypesFinder
{
	private static Assembly[]? _currentDomainAssemblies;
	private static IList<Type>? _currentDomainAssembliesTypes;

	/// <summary>
	/// Gets or sets the excluded assemblies prefixes.
	/// </summary>
	/// <exception cref="ArgumentNullException">value</exception>
	public static IList<string> ExcludedAssembliesPrefixes { get; } =
	[
		"mscorlib",
		"System",
		"Microsoft",
		"AspNet",
		"DotNet",
		"Simplify"
	];

	private static IList<Assembly> CurrentDomainAssemblies => _currentDomainAssemblies ??= AppDomain.CurrentDomain.GetAssemblies();

	private static IList<Type> CurrentDomainAssembliesTypes => _currentDomainAssembliesTypes
#pragma warning disable S2365
		??= CurrentDomainAssemblies.GetAssembliesTypes(ExcludedAssembliesPrefixes).ToList();

#pragma warning restore S2365

	/// <summary>
	/// Finds the type derived from specified type in the current domain assemblies.
	/// </summary>
	/// <typeparam name="T">Type to find types derived from.</typeparam>
	public static Type? FindTypeDerivedFrom<T>()
	{
		var type = typeof(T);

		return CurrentDomainAssembliesTypes.FirstOrDefault(t => t.IsDerivedFrom(type));
	}

	/// <summary>
	/// Finds the all types derived from specified type in the current domain assemblies.
	/// </summary>
	/// <typeparam name="T">The type.</typeparam>
	public static IEnumerable<Type> FindTypesDerivedFrom<T>() => FindTypesDerivedFrom(typeof(T));

	/// <summary>
	/// Finds the types derived from.
	/// </summary>
	/// <param name="types">The types.</param>
	public static IEnumerable<Type> FindTypesDerivedFrom(IEnumerable<Type> types) => types
		.SelectMany(FindTypesDerivedFrom);

	/// <summary>
	/// Finds the types derived from.
	/// </summary>
	/// <param name="types">The types.</param>
	public static IEnumerable<Type> FindTypesDerivedFrom(params Type[] types) => types
		.SelectMany(FindTypesDerivedFrom);

	/// <summary>
	/// Finds all the types derived from specified type in the current domain assemblies.
	/// </summary>
	/// <param name="type">Type to find types derived from.</param>
	public static IEnumerable<Type> FindTypesDerivedFrom(Type type) =>
		CurrentDomainAssembliesTypes
			.Where(t => t.IsDerivedFrom(type));

	/// <summary>
	/// Gets the controller types to ignore.
	/// </summary>
	public static IList<Type> GetControllerTypesToIgnore()
	{
		var ignoreContainingClass = CurrentDomainAssembliesTypes
			.FirstOrDefault(t => t.IsDefined(typeof(IgnoreControllersAttribute), true));

		if (ignoreContainingClass == null)
			return [];

		var attributes = ignoreContainingClass.GetCustomAttributes(typeof(IgnoreControllersAttribute), false);

		return ((IgnoreControllersAttribute)attributes[0]).Types;
	}

	/// <summary>
	/// Gets the types to ignore.
	/// </summary>
	public static IList<Type> GetIgnoredIocRegistrationTypes()
	{
		var ignoreContainingClass = CurrentDomainAssembliesTypes
			.FirstOrDefault(t => t.IsDefined(typeof(IgnoreTypesRegistrationAttribute), true));

		if (ignoreContainingClass == null)
			return [];

		var attributes = ignoreContainingClass.GetCustomAttributes(typeof(IgnoreTypesRegistrationAttribute), false);

		return ((IgnoreTypesRegistrationAttribute)attributes[0]).Types;
	}

	/// <summary>
	/// Clean up the loaded information about assemblies and types.
	/// </summary>
	public static void CleanLoadedTypesAndAssembliesInfo()
	{
		_currentDomainAssemblies = null;
		_currentDomainAssembliesTypes = null;
	}
}