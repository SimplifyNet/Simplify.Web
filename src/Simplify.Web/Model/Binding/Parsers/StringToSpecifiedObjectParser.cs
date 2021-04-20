using System;
using System.Globalization;

namespace Simplify.Web.Model.Binding.Parsers
{
	/// <summary>
	/// Provides string to specified type parsing
	/// </summary>
	public class StringToSpecifiedObjectParser
	{
		/// <summary>
		/// Determine variable type and parses it from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="parsingType">Type to parse.</param>
		/// <param name="format">The format.</param>
		/// <returns></returns>
		/// <exception cref="ModelNotSupportedException"></exception>
		public static object? ParseUndefined(string value, Type parsingType, string? format = null)
		{
			if (parsingType == typeof(string))
				return ParseString(value);

			if (parsingType == typeof(int))
				return ParseInt(value);

			if (parsingType == typeof(int?))
				return ParseNullableInt(value);

			if (parsingType == typeof(bool))
				return ParseBool(value);

			if (parsingType == typeof(bool?))
				return ParseNullableBool(value);

			if (parsingType == typeof(decimal))
				return ParseDecimal(value);

			if (parsingType == typeof(decimal?))
				return ParseNullableDecimal(value);

			if (parsingType == typeof(long))
				return ParseLong(value);

			if (parsingType == typeof(long?))
				return ParseNullableLong(value);

			if (parsingType == typeof(DateTime))
				return ParseDateTime(value, format);

			if (parsingType == typeof(DateTime?))
				return ParseNullableDateTime(value, format);

			if (parsingType.IsEnum)
				return ParseEnum(value, parsingType);

			throw new ModelNotSupportedException($"String parsing failed, not supported type: '{parsingType}', value '{value}'");
		}

		/// <summary>
		/// Parses the string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string? ParseString(string value) => string.IsNullOrEmpty(value) ? null : value;

		/// <summary>
		/// Parses the boolean from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static bool ParseBool(string value)
		{
			if (string.IsNullOrEmpty(value))
				return default;

			value = value.ToLower();

			if (value == "on")
				return true;

			if (bool.TryParse(value, out var buffer))
				return buffer;

			throw new ModelBindingException($"String to bool parsing failed, value: '{value}'");
		}

		/// <summary>
		/// Parses the nullable boolean from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static bool? ParseNullableBool(string value)
		{
			if (string.IsNullOrEmpty(value))
				return null;

			value = value.ToLower();

			if (value == "on")
				return true;

			if (bool.TryParse(value, out var buffer))
				return buffer;

			throw new ModelBindingException($"String to nullable bool parsing failed, value: '{value}'");
		}

		/// <summary>
		/// Parses the int from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		/// <exception cref="ModelBindingException"></exception>
		public static int ParseInt(string value)
		{
			if (string.IsNullOrEmpty(value))
				return default;

			if (int.TryParse(value, out var buffer))
				return buffer;

			throw new ModelBindingException($"String to int parsing failed, value: '{value}'");
		}

		/// <summary>
		/// Parses the nullable int from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		/// <exception cref="ModelBindingException"></exception>
		public static int? ParseNullableInt(string value)
		{
			if (string.IsNullOrEmpty(value))
				return null;

			if (int.TryParse(value, out var buffer))
				return buffer;

			throw new ModelBindingException($"String to nullable int parsing failed, value: '{value}'");
		}

		/// <summary>
		/// Parses the decimal from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		/// <exception cref="ModelBindingException"></exception>
		public static decimal ParseDecimal(string value)
		{
			if (string.IsNullOrEmpty(value))
				return default;

			if (decimal.TryParse(value, out var buffer))
				return buffer;

			throw new ModelBindingException($"String to decimal parsing failed, value: '{value}'");
		}

		/// <summary>
		/// Parses the nullable decimal from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		/// <exception cref="ModelBindingException"></exception>
		public static decimal? ParseNullableDecimal(string value)
		{
			if (string.IsNullOrEmpty(value))
				return null;

			if (decimal.TryParse(value, out var buffer))
				return buffer;

			throw new ModelBindingException($"String to nullable decimal parsing failed, value: '{value}'");
		}

		/// <summary>
		/// Parses the long from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		/// <exception cref="ModelBindingException"></exception>
		public static long ParseLong(string value)
		{
			if (string.IsNullOrEmpty(value))
				return default;

			if (long.TryParse(value, out var buffer))
				return buffer;

			throw new ModelBindingException($"String to long parsing failed, value: '{value}'");
		}

		/// <summary>
		/// Parses the nullable long from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		/// <exception cref="ModelBindingException"></exception>
		public static long? ParseNullableLong(string value)
		{
			if (string.IsNullOrEmpty(value))
				return null;

			if (long.TryParse(value, out var buffer))
				return buffer;

			throw new ModelBindingException($"String to nullable long parsing failed, value: '{value}'");
		}

		/// <summary>
		/// Parses the date time from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="format">The format.</param>
		/// <returns></returns>
		/// <exception cref="ModelBindingException"></exception>
		public static DateTime ParseDateTime(string value, string? format = null)
		{
			if (string.IsNullOrEmpty(value))
				return default;

			DateTime buffer;

			if (format != null)
			{
				if (DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out buffer))
					return buffer;

				throw new ModelBindingException(
					$"String to DateTime parsing failed, required format is '{format}', value: '{value}'");
			}

			if (DateTime.TryParse(value, out buffer))
				return buffer;

			throw new ModelBindingException($"String to DateTime parsing failed, value: '{value}'");
		}

		/// <summary>
		/// Parses the date time from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="format">The format.</param>
		/// <returns></returns>
		/// <exception cref="ModelBindingException"></exception>
		public static DateTime? ParseNullableDateTime(string value, string? format = null)
		{
			if (string.IsNullOrEmpty(value))
				return null;

			DateTime buffer;

			if (format != null)
			{
				if (DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out buffer))
					return buffer;

				throw new ModelBindingException(
					$"String to nullable DateTime parsing failed, required format is '{format}', value: '{value}'");
			}

			if (DateTime.TryParse(value, out buffer))
				return buffer;

			throw new ModelBindingException($"String to nullable DateTime parsing failed, value: '{value}'");
		}

		/// <summary>
		/// Parses the enum from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="enumType">Type of the enum.</param>
		/// <returns></returns>
		public static object ParseEnum(string value, Type enumType)
		{
			return Enum.Parse(enumType, value);
		}

		/// <summary>
		/// Determines whether specified type is valid for parsing.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static bool IsTypeValidForParsing(Type type)
		{
			if (type == typeof(string))
				return true;

			if (type == typeof(int))
				return true;

			if (type == typeof(int?))
				return true;

			if (type == typeof(bool))
				return true;

			if (type == typeof(bool?))
				return true;

			if (type == typeof(decimal))
				return true;

			if (type == typeof(decimal?))
				return true;

			if (type == typeof(long))
				return true;

			if (type == typeof(long?))
				return true;

			if (type == typeof(DateTime))
				return true;

			if (type == typeof(DateTime?))
				return true;

			if (type.IsEnum)
				return true;

			return false;
		}
	}
}