using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Simplify.Web.Old.Attributes.Setup;

namespace Simplify.Web.Old.Meta;

/// <summary>
/// Provides Simplify.Web types finder among all loaded assemblies.
/// </summary>
public static class SimplifyWebTypesFinder
{
	private static Assembly[]? _currentDomainAssemblies;
	private static IEnumerable<Type>? _currentDomainAssembliesTypes;

	/// <summary>
	/// Gets or sets the excluded assemblies prefixes.
	/// </summary>
	/// <value>
	/// The excluded assemblies prefixes.
	/// </value>
	/// <exception cref="ArgumentNullException">value</exception>
	public static IList<string> ExcludedAssembliesPrefixes { get; } = new List<string>
	{
		"mscorlib",
		"System",
		"Microsoft",
		"AspNet",
		"DotNet",
		"Simplify"
	};

	private static IEnumerable<Assembly> CurrentDomainAssemblies => _currentDomainAssemblies ??= AppDomain.CurrentDomain.GetAssemblies();

	private static IEnumerable<Type> CurrentDomainAssembliesTypes => _currentDomainAssembliesTypes ??= GetAssembliesTypes(CurrentDomainAssemblies);

	/// <summary>
	/// Finds the type derived from specified type in current domain assemblies.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static Type? FindTypeDerivedFrom<T>()
	{
		var type = typeof(T);

		return CurrentDomainAssembliesTypes.FirstOrDefault(t => t.BaseType != null && t.BaseType.FullName == type.FullName);
	}

	/// <summary>
	/// Finds the all types derived from specified type in current domain assemblies.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public static IList<Type> FindTypesDerivedFrom<T>() => FindTypesDerivedFrom(typeof(T));

	/// <summary>
	/// Finds the all types derived from specified type in current domain assemblies.
	/// </summary>
	/// <param name="type">The type.</param>
	/// <returns></returns>
	public static IList<Type> FindTypesDerivedFrom(Type type) =>
		CurrentDomainAssembliesTypes.Where(t =>
		{
			if (t.IsAbstract)
				return false;

			if (t.BaseType == null)
				return false;

			if (t.BaseType.IsTypeOf(type))
				return true;

			if (t.BaseType.BaseType == null)
				return false;

			if (t.BaseType.BaseType.IsTypeOf(type))
				return true;

			return false;
		}).ToList();

	/// <summary>
	/// Gets all types.
	/// </summary>
	/// <returns></returns>
	public static IList<Type> GetAllTypes() => CurrentDomainAssembliesTypes.ToList();

	/// <summary>
	/// Gets the types to ignore.
	/// </summary>
	/// <returns></returns>
	public static IList<Type> GetTypesToIgnore()
	{
		var typesToIgnore = new List<Type>();

		var ignoreContainingClass = GetAllTypes()
			.FirstOrDefault(t => t.IsDefined(typeof(IgnoreTypesRegistrationAttribute), true));

		if (ignoreContainingClass == null)
			return typesToIgnore;

		var attributes = ignoreContainingClass.GetCustomAttributes(typeof(IgnoreTypesRegistrationAttribute), false);

		typesToIgnore.AddRange(((IgnoreTypesRegistrationAttribute)attributes[0]).Types);

		return typesToIgnore;
	}

	/// <summary>
	/// Clean up the loaded information about assemblies and types
	/// </summary>
	public static void CleanLoadedTypesAndAssembliesInfo()
	{
		_currentDomainAssemblies = null;
		_currentDomainAssembliesTypes = null;
	}

	private static bool IsTypeOf(this Type t, Type type)
	{
		switch (t.IsGenericType)
		{
			case false when t == type:
			case true when t.GetGenericTypeDefinition() == type:
				return true;

			default:
				return false;
		}
	}

	private static IEnumerable<Type> GetAssembliesTypes(IEnumerable<Assembly> assemblies)
	{
		var types = new List<Type>();

		foreach (var assembly in assemblies
			.Where(assembly => !ExcludedAssembliesPrefixes
			.Any(prefix => assembly.FullName != null && assembly.FullName.StartsWith(prefix))))
			types.AddRange(assembly.GetTypes());

		return types;
	}
}