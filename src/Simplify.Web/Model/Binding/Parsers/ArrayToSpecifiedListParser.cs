using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Simplify.Web.Model.Binding.Parsers;

/// <summary>
/// Provides string array to list parsing
/// </summary>
public class ArrayToSpecifiedListParser
{
	/// <summary>
	/// Determines whether specified type is valid for parsing.
	/// </summary>
	/// <param name="type">The type.</param>
	/// <returns></returns>
	/// <exception cref="ModelNotSupportedException">Exception throws in case of undefined list type</exception>
	public static bool IsTypeValidForParsing(Type type)
	{
		if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(IList<>))
			return false;

		var genericType = GetGenericListType(type);

		if (!StringToSpecifiedObjectParser.IsTypeValidForParsing(genericType!))
			throw new ModelNotSupportedException($"Not supported list property type of: '{genericType}'");

		return true;
	}

	/// <summary>
	/// Parses the undefined values types from string array to list.
	/// </summary>
	/// <param name="values">The values.</param>
	/// <param name="type">The type to parse to.</param>
	/// <param name="format">The format.</param>
	/// <returns></returns>
	/// <exception cref="ModelNotSupportedException"></exception>
	public static object? ParseUndefined(string[] values, Type type, string? format = null)
	{
		var parsingType = GetGenericListType(type);

		if (parsingType == null)
			return null;

		if (parsingType == typeof(string))
			return values.Select(StringToSpecifiedObjectParser.ParseString).ToList();

		if (parsingType == typeof(int))
			return values.Select(StringToSpecifiedObjectParser.ParseInt).ToList();

		if (parsingType == typeof(int?))
			return values.Select(StringToSpecifiedObjectParser.ParseNullableInt).ToList();

		if (parsingType == typeof(bool))
			return values.Select(StringToSpecifiedObjectParser.ParseBool).ToList();

		if (parsingType == typeof(bool?))
			return values.Select(StringToSpecifiedObjectParser.ParseNullableBool).ToList();

		if (parsingType == typeof(decimal))
			return values.Select(StringToSpecifiedObjectParser.ParseDecimal).ToList();

		if (parsingType == typeof(decimal?))
			return values.Select(StringToSpecifiedObjectParser.ParseNullableDecimal).ToList();

		if (parsingType == typeof(long))
			return values.Select(StringToSpecifiedObjectParser.ParseLong).ToList();

		if (parsingType == typeof(long?))
			return values.Select(StringToSpecifiedObjectParser.ParseNullableLong).ToList();

		if (parsingType == typeof(DateTime))
			return values.Select(x => StringToSpecifiedObjectParser.ParseDateTime(x, format)).ToList();

		if (parsingType == typeof(DateTime?))
			return values.Select(x => StringToSpecifiedObjectParser.ParseNullableDateTime(x, format)).ToList();

		if (parsingType.IsEnum)
		{
			var listType = typeof(List<>).MakeGenericType(parsingType);
			var list = Activator.CreateInstance(listType);
			var methodInfo = listType.GetMethod("Add")!;

			foreach (var value in values)
				methodInfo.Invoke(list, new[] { StringToSpecifiedObjectParser.ParseEnum(value, parsingType) });

			return list;
		}

		throw new ModelNotSupportedException($"Array parsing failed, not supported type: '{type}'");
	}

	/// <summary>
	/// Gets the type of the generic list.
	/// </summary>
	/// <param name="type">The type.</param>
	/// <returns></returns>
	public static Type? GetGenericListType(Type type)
	{
		var genericTypeArguments = type.GetTypeInfo().GenericTypeArguments;

		return genericTypeArguments.Length > 0 ? genericTypeArguments[0] : null;
	}
}