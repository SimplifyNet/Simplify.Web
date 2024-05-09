#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Simplify.Web.Model.Binding.Attributes;

namespace Simplify.Web.Model.Binding.Parsers;

/// <summary>
/// Provides the list of key value pair to model binding.
/// </summary>
public static class ListToModelParser
{
	/// <summary>
	/// Parses thw list and creates a model.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public static T Parse<T>(IList<KeyValuePair<string, string[]>> source)
	{
		var type = typeof(T);

		var obj = Activator.CreateInstance<T>();

		source = LowerCaseRequest(source);

		foreach (var propInfo in type.GetProperties())
		{
			var propertyInfo = propInfo;

			if (IsExcluded(propertyInfo))
				continue;

			propInfo.SetValue(obj, ParseProperty(propInfo, source.FirstOrDefault(x => x.Key == GetPropertyName(propertyInfo))));
		}

		return obj;
	}

	private static IList<KeyValuePair<string, string[]>> LowerCaseRequest(IEnumerable<KeyValuePair<string, string[]>> source) =>
		source.Select(x => new KeyValuePair<string, string[]>(x.Key?.ToLower(), x.Value)).ToList();

	private static string GetPropertyName(MemberInfo propertyInfo) =>
		(GetBindPropertyName(propertyInfo) ?? propertyInfo.Name).ToLower();

	private static string GetBindPropertyName(ICustomAttributeProvider propertyInfo)
	{
		var attributes = propertyInfo.GetCustomAttributes(typeof(BindPropertyAttribute), false);

		return attributes.Length == 0
		 ? null
		 : ((BindPropertyAttribute)attributes[0]).FieldName;
	}

	private static string TryGetFormat(ICustomAttributeProvider propertyInfo)
	{
		var attributes = propertyInfo.GetCustomAttributes(typeof(FormatAttribute), false);

		return attributes.Length == 0
			? null
			: ((FormatAttribute)attributes[0]).Format;
	}

	private static bool IsExcluded(ICustomAttributeProvider propertyInfo) =>
		propertyInfo.GetCustomAttributes(typeof(ExcludeAttribute), false).Length != 0;

	private static object ParseProperty(PropertyInfo propertyInfo, KeyValuePair<string, string[]> keyValuePair)
	{
		if (keyValuePair.Equals(default(KeyValuePair<string, string[]>)) || keyValuePair.Value.Length == 0)
			return null;

		return ArrayToSpecifiedListParser.IsTypeValidForParsing(propertyInfo.PropertyType)
			? ArrayToSpecifiedListParser.ParseUndefined(keyValuePair.Value, propertyInfo.PropertyType, TryGetFormat(propertyInfo))
			: StringToSpecifiedObjectParser.ParseUndefined(string.Join(",", keyValuePair.Value), propertyInfo.PropertyType, TryGetFormat(propertyInfo));
	}
}