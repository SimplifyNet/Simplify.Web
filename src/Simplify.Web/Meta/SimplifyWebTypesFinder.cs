using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Simplify.Web.Attributes.Setup;
using Simplify.Web.System;

namespace Simplify.Web.Meta;

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
		??= CurrentDomainAssemblies.GetAssembliesTypes(ExcludedAssembliesPrefixes).ToList();

	/// <summary>
	/// Finds the type derived from specified type in the current domain assemblies.
	/// </summary>
	/// <typeparam name="T">Type to find types derived from.</typeparam>
	public static Type? FindTypeDerivedFrom<T>()
	{
		var type = typeof(T);

		return CurrentDomainAssembliesTypes.FirstOrDefault(t => t.BaseType != null && t.BaseType.FullName == type.FullName);
	}

	/// <summary>
	/// Finds the all types derived from specified type in the current domain assemblies.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public static IList<Type> FindTypesDerivedFrom<T>() => FindTypesDerivedFrom(typeof(T));

	public static IList<Type> FindTypesDerivedFrom(params Type[] types) => types.SelectMany(x => FindTypesDerivedFrom(x)).ToList();

	/// <summary>
	/// Finds all the types derived from specified type in the current domain assemblies.
	/// </summary>
	/// <param name="type">Type to find types derived from.</param>
	public static IList<Type> FindTypesDerivedFrom(Type type) =>
		CurrentDomainAssembliesTypes.Where(t => t.IsTypeDerivedFrom(type)).ToList();

	/// <summary>
	/// Gets the controller types to ignore.
	/// </summary>
	public static IEnumerable<Type> GetControllerTypesToIgnore()
	{
		var ignoreContainingClass = CurrentDomainAssembliesTypes
			.FirstOrDefault(t => t.IsDefined(typeof(IgnoreControllersAttribute), true));

		if (ignoreContainingClass == null)
			return new List<Type>();

		var attributes = ignoreContainingClass.GetCustomAttributes(typeof(IgnoreControllersAttribute), false);

		return ((IgnoreControllersAttribute)attributes[0]).Types;
	}

	/// <summary>
	/// Gets the types to ignore.
	/// </summary>
	public static IEnumerable<Type> GetTypesToIgnore()
	{
		var ignoreContainingClass = CurrentDomainAssembliesTypes
			.FirstOrDefault(t => t.IsDefined(typeof(IgnoreTypesRegistrationAttribute), true));

		if (ignoreContainingClass == null)
			return new List<Type>();

		var attributes = ignoreContainingClass.GetCustomAttributes(typeof(IgnoreTypesRegistrationAttribute), false);

		return ((IgnoreTypesRegistrationAttribute)attributes[0]).Types;
	}

	/// <summary>
	/// Clean up the loaded information about assemblies and types
	/// </summary>
	public static void CleanLoadedTypesAndAssembliesInfo()
	{
		_currentDomainAssemblies = null;
		_currentDomainAssembliesTypes = null;
	}
}