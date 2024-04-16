﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Simplify.Web.Attributes.Setup;
using Simplify.Web.Utils.Assemblies;
using Simplify.Web.Utils.Types;

namespace Simplify.Web.Meta;

/// <summary>
/// Provides the Simplify.Web types finder among all loaded assemblies.
/// </summary>
public static class SimplifyWebTypesFinder
{
	private static Assembly[]? _currentDomainAssemblies;
	private static IEnumerable<Type>? _currentDomainAssembliesTypes;

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

	private static IEnumerable<Assembly> CurrentDomainAssemblies => _currentDomainAssemblies ??= AppDomain.CurrentDomain.GetAssemblies();
	private static IEnumerable<Type> CurrentDomainAssembliesTypes => _currentDomainAssembliesTypes ??= CurrentDomainAssemblies.GetAssembliesTypes(ExcludedAssembliesPrefixes);

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

	/// <summary>
	/// Finds all the types derived from specified type in the current domain assemblies.
	/// </summary>
	/// <param name="type">Type to find types derived from.</param>
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
	/// Gets all the types.
	/// </summary>
	public static IList<Type> GetAllTypes() => CurrentDomainAssembliesTypes.ToList();

	/// <summary>
	/// Gets the types to ignore.
	/// </summary>
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
}