using System;
using System.Collections.Generic;
using System.Linq;

namespace Simplify.Web.System;

/// <summary>
/// Provides the string converter.
/// </summary>
public static class StringConverter
{
	/// <summary>
	/// Provides the value converters list
	/// </summary>
	public static readonly IReadOnlyDictionary<Type, Func<string, object?>> ValueConverters =
		new Dictionary<Type, Func<string, object?>>
		{
			{ typeof(string), sourceValue => sourceValue },
			{ typeof(int), GetIntParameterValue },
			{ typeof(long), GetLongParameterValue },
			{ typeof(decimal), GetDecimalParameterValue },
			{ typeof(float), GetFloatParameterValue },
			{ typeof(double), GetDoubleParameterValue },
			{ typeof(bool), GetBoolParameterValue },
			{ typeof(string[]), GetStringArrayParameterValue },
			{ typeof(int[]), GetIntArrayParameterValue },
			{ typeof(decimal[]), GetDecimalArrayParameterValue },
			{ typeof(bool[]), GetBoolArrayParameterValue }
		};

	/// <summary>
	/// Tries the convert the string to the object of specified type.
	/// </summary>
	/// <param name="destinationType">Type of the destination.</param>
	/// <param name="sourceValue">The source value.</param>
	public static object? TryConvert(Type destinationType, string sourceValue) =>
		ValueConverters.TryGetValue(destinationType, out var converter)
			? converter(sourceValue)
			: null;

	private static object? GetIntParameterValue(string source)
	{
		if (!int.TryParse(source, out var buffer))
			return null;

		return buffer;
	}

	private static object? GetLongParameterValue(string source)
	{
		if (!long.TryParse(source, out var buffer))
			return null;

		return buffer;
	}

	private static object? GetDecimalParameterValue(string source)
	{
		if (!decimal.TryParse(source, out var buffer))
			return null;

		return buffer;
	}

	private static object? GetFloatParameterValue(string source)
	{
		if (!float.TryParse(source, out var buffer))
			return null;

		return buffer;
	}

	private static object? GetDoubleParameterValue(string source)
	{
		if (!double.TryParse(source, out var buffer))
			return null;

		return buffer;
	}

	private static object? GetBoolParameterValue(string source)
	{
		if (!bool.TryParse(source, out var buffer))
			return null;

		return buffer;
	}

	private static IList<string> GetStringArrayParameterValue(string source) =>
		source.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

	private static IList<int> GetIntArrayParameterValue(string source) =>
		source.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
			.Select(GetIntParameterValue)
			.Where(x => x != null)
			.Cast<int>()
			.ToList();

	private static IList<decimal> GetDecimalArrayParameterValue(string source) =>
		source.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
			.Select(GetDecimalParameterValue)
			.Where(x => x != null)
			.Cast<decimal>()
			.ToList();

	private static IList<bool> GetBoolArrayParameterValue(string source) =>
		source.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
			.Select(GetBoolParameterValue)
			.Where(x => x != null)
			.Cast<bool>()
			.ToList();
}