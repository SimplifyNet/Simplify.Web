using System.Collections.Generic;
using System.Dynamic;

namespace Simplify.Web.System;

/// <summary>
/// Provides the dictionary extensions.
/// </summary>
public static class DictionaryExtensions
{
	/// <summary>
	/// Converts to dictionary to <see cref="ExpandoObject" />.
	/// </summary>
	/// <param name="dictionary">The dictionary.</param>
	public static ExpandoObject ToExpandoObject(this IReadOnlyDictionary<string, object> dictionary)
	{
		var expando = new ExpandoObject();
		var expandoDIc = (IDictionary<string, object>)expando!;

		foreach (var item in dictionary)
			expandoDIc.Add(item.Key, item.Value);

		return expando;
	}
}