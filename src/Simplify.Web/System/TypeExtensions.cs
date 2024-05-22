using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplify.Web.System;

/// <summary>
/// Provides the Type extensions
/// </summary>
public static class TypeExtensions
{
	/// <summary>
	/// Determines whether one type is derivative of another.
	/// </summary>
	/// <param name="t">The type.</param>
	/// <param name="type">The type.</param>
	public static bool IsTypeOf(this Type t, Type type)
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

	public static bool IsDerivedFrom(this Type t, params Type[] types) => types.Any(t.IsDerivedFrom);

	public static bool IsDerivedFrom(this Type t, Type type)
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
	}

	public static string GetTypeNamesAsString(this IEnumerable<Type> types) => string.Join(", ", types.Select(type => type.Name));
}