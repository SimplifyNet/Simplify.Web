using System.Collections.Generic;
using System.Dynamic;

namespace Simplify.Web.System;

public static class DictionaryExtensions
{
	public static ExpandoObject ToExpandoObject(this IReadOnlyDictionary<string, object> dictionary)
	{
		var expando = new ExpandoObject();
		var expandoDIc = (IDictionary<string, object>)expando!;

		foreach (var item in dictionary)
			expandoDIc.Add(item.Key, item.Value);

		return expando;
	}
}