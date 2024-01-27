using System.Diagnostics;
using System.Dynamic;
using BenchmarkDotNet.Attributes;

namespace Simplify.Web.Benchmark;

[MemoryDiagnoser]
public class ExpandoObjectAndDictionaryBenchmarks

{
	[Params(1, 5, 20, 100, 300, 1000)]
	public int NumValues;

	[Benchmark]
	public void ExpandoBased_Combined_Test()
	{
		var expandoObject = CreateAndFillExpando();

		TestDynamic(expandoObject);
		TesDictionary(expandoObject);
	}

	[Benchmark]
	public void ExpandoBased_Dynamic_Test()
	{
		var expandoObject = CreateAndFillExpando();

		TestDynamic(expandoObject);
	}

	[Benchmark]
	public void ExpandoBased_Dictionary_Test()
	{
		var expandoObject = CreateAndFillExpando();

		TesDictionary(expandoObject);
	}

	[Benchmark]
	public void DictionaryBased_Combined_Test()
	{
		var dictionary = CreateAndFillDictionary();

		TestDynamic(ToExpando(dictionary));
		TesDictionary(dictionary);
	}

	[Benchmark]
	public void DictionaryBased_Expando_Test()
	{
		var dictionary = CreateAndFillDictionary();

		TestDynamic(ToExpando(dictionary));
	}

	[Benchmark]
	public void DictionaryBased_Dictionary_Test()
	{
		var dictionary = CreateAndFillDictionary();

		TesDictionary(dictionary);
	}
	private void TestDynamic(dynamic list)
	{
		for (int i = 0; i < NumValues; i++)
		{
			string value = list.Key0;
			Trace.WriteLine(value);
		}
	}

	private void TesDictionary(IDictionary<string, object> list)
	{
		for (int i = 0; i < NumValues; i++)
		{
			string value = list[$"Key{i}"].ToString();
			Trace.WriteLine(value);
		}
	}

	private IDictionary<string, object> CreateAndFillExpando()
	{
		var expandoDict = (IDictionary<string, object>)new ExpandoObject();

		for (int i = 0; i < NumValues; i++)
			expandoDict[$"Key{i}"] = $"Value{i}";

		return expandoDict;
	}

	private Dictionary<string, object> CreateAndFillDictionary()
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