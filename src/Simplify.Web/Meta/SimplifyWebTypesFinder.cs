using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable

namespace Simplify.Web.Meta
{
	/// <summary>
	/// Provides Simplify.Web types finder among all loaded assemblies
	/// </summary>
	public static class SimplifyWebTypesFinder
	{
		private static Assembly[] _currentDomainAssemblies;
		private static IEnumerable<Type> _currentDomainAssembliesTypes;

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

		private static IEnumerable<Assembly> CurrentDomainAssemblies => _currentDomainAssemblies ?? (_currentDomainAssemblies = AppDomain.CurrentDomain.GetAssemblies());

		private static IEnumerable<Type> CurrentDomainAssembliesTypes => _currentDomainAssembliesTypes ?? (_currentDomainAssembliesTypes = GetAssembliesTypes(CurrentDomainAssemblies));

		/// <summary>
		/// Finds the type derived from specified type in current domain assemblies.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static Type FindTypeDerivedFrom<T>()
		{
			var type = typeof(T);

			return CurrentDomainAssembliesTypes.FirstOrDefault(t => t.BaseType != null && t.BaseType.FullName == type.FullName);
		}

		/// <summary>
		/// Finds the all types derived from specified type in current domain assemblies.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static IList<Type> FindTypesDerivedFrom<T>()
		{
			return FindTypesDerivedFrom(typeof(T));
		}

		/// <summary>
		/// Finds the all types derived from specified type in current domain assemblies.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static IList<Type> FindTypesDerivedFrom(Type type)
		{
			return CurrentDomainAssembliesTypes.Where(t => t.BaseType != null
														   && ((t.BaseType.IsGenericType == false && t.BaseType == type)
															   ||
															   (t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == type)))
				.ToList();
		}

		/// <summary>
		/// Gets all types.
		/// </summary>
		/// <returns></returns>
		public static IList<Type> GetAllTypes()
		{
			return CurrentDomainAssembliesTypes.ToList();
		}

		/// <summary>
		/// Clean up the loaded information about assemblies and types
		/// </summary>
		public static void CleanLoadedTypesAndAssembliesInfo()
		{
			_currentDomainAssemblies = null;
			_currentDomainAssembliesTypes = null;
		}

		private static IEnumerable<Type> GetAssembliesTypes(IEnumerable<Assembly> assemblies)
		{
			var types = new List<Type>();

			foreach (var assembly in assemblies.Where(assembly => !ExcludedAssembliesPrefixes.Any(prefix => assembly.FullName.StartsWith(prefix))))
				types.AddRange(assembly.GetTypes());

			return types;
		}
	}
}