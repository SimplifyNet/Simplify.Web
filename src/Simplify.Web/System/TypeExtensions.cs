using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplify.Web.System;

/// <summary>
/// Provides the <see cref="Type" /> extensions.
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

	/// <summary>
	/// Determines whether the type is derived from one of the specified types.
	/// </summary>
	/// <param name="t">The t.</param>
	/// <param name="types">The types.</param>
	/// <returns>
	///   <c>true</c> if  the type is derived from one of the specified types; otherwise, <c>false</c>.
	/// </returns>
	public static bool IsDerivedFrom(this Type t, params Type[] types) => types.Any(t.IsDerivedFrom);

	/// <summary>
	/// Determines whether the type is derived from other type.
	/// </summary>
	/// <param name="t">The t.</param>
	/// <param name="type">The type.</param>
	/// <returns>
	///   <c>true</c> if the type is derived from other type; otherwise, <c>false</c>.
	/// </returns>
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

	/// <summary>
	/// Gets the type names as string.
	/// </summary>
	/// <param name="types">The types.</param>
	public static string GetTypeNamesAsString(this IEnumerable<Type> types) => string.Join(", ", types.Select(type => type.Name));
}