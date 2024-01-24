using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using BenchmarkDotNet.Attributes;

namespace Simplify.Web.Benchmark;

[MemoryDiagnoser]
public class ExpandoObjectAndDictionaryBenchmarks
{
	private const int NumValues = 1;

	[Benchmark]
	public void ExpandoObjectTest()
	{
		var expandoObject = CreateAndFillExpando();

		dynamic expandoDynamicField = expandoObject;

		for (int i = 0; i < NumValues; i++)
		{
			string value = expandoDynamicField.Key0;
			Trace.WriteLine(value);
		}

		var expandoDictionaryField = expandoObject;

		for (int i = 0; i < NumValues; i++)
		{
			string value = expandoDictionaryField[$"Key{i}"].ToString();
			Trace.WriteLine(value);
		}
	}

	[Benchmark]
	public void DictionaryTest()
	{
		var dictionary = CreateAndFillDictionary();

		dynamic dictionaryDynamicField = ToExpando(dictionary);

		for (int i = 0; i < NumValues; i++)
		{
			string value = dictionaryDynamicField.Key0;
			Trace.WriteLine(value);
		}

		for (int i = 0; i < NumValues; i++)
		{
			string value = dictionary[$"Key{i}"].ToString();
			Trace.WriteLine(value);
		}
	}

	private static IDictionary<string, object> CreateAndFillExpando()
	{
		var expandoDict = (IDictionary<string, object>)new ExpandoObject();

		for (int i = 0; i < NumValues; i++)
			expandoDict[$"Key{i}"] = $"Value{i}";

		return expandoDict;
	}

	private static Dictionary<string, object> CreateAndFillDictionary()
	{
		var dictionary = new Dictionary<string, object>();

		for (int i = 0; i < NumValues; i++)
			dictionary[$"Key{i}"] = $"Value{i}";

		return dictionary;
	}

	private static ExpandoObject ToExpando(Dictionary<string, object> dictionary)
	{
		var expando = new ExpandoObject();
		var expandoDict = (IDictionary<string, object>)expando;

		foreach (var kvp in dictionary)
			expandoDict[kvp.Key] = kvp.Value;

		return expando;
	}
}