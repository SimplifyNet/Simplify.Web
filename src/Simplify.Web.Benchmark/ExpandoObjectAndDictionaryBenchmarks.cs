using System.Dynamic;
using BenchmarkDotNet.Attributes;

#pragma warning disable S1104

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
		TestDictionary(expandoObject);
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

		TestDictionary(expandoObject);
	}

	[Benchmark]
	public void DictionaryBased_Combined_Test()
	{
		var dictionary = CreateAndFillDictionary();

		TestDynamic(ToExpando(dictionary));
		TestDictionary(dictionary);
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

		TestDictionary(dictionary);
	}

	private static ExpandoObject ToExpando(Dictionary<string, object> dictionary)
	{
		var expando = new ExpandoObject();
		var expandoDict = (IDictionary<string, object>)expando;

		foreach (var kvp in dictionary)
			expandoDict[kvp.Key] = kvp.Value;

		return expando;
	}

	private void TestDynamic(dynamic list)
	{
		for (var i = 0; i < NumValues; i++)
		{
			var value = list.Key0;
			Console.WriteLine(value);
		}
	}

	private void TestDictionary(IDictionary<string, object> list)
	{
		for (var i = 0; i < NumValues; i++)
		{
			var value = list[$"Key{i}"].ToString();
			Console.WriteLine(value);
		}
	}

	private IDictionary<string, object> CreateAndFillExpando()
	{
		var expandoDict = (IDictionary<string, object>)new ExpandoObject();

		for (var i = 0; i < NumValues; i++)
			expandoDict[$"Key{i}"] = $"Value{i}";

		return expandoDict;
	}

	private Dictionary<string, object> CreateAndFillDictionary()
	{
		var dictionary = new Dictionary<string, object>();

		for (var i = 0; i < NumValues; i++)
			dictionary[$"Key{i}"] = $"Value{i}";

		return dictionary;
	}
}