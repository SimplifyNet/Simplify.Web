using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using NUnit.Framework;

#nullable disable

namespace Simplify.Web.Tests.Benchmark;

[TestFixture]
[Category("Benchmark")]
public class ExpandoVsDictionaryBenchmark
{
	[TestCase(1)]
	[TestCase(5)]
	[TestCase(20)]
	[TestCase(100)]
	[TestCase(300)]
	[TestCase(1000)]
	public void ExpandoObjectTest(int numValues)
	{
		var overallStopwatch = new Stopwatch();
		var dynamicReadStopwatch = new Stopwatch();
		var dictionaryReadStopwatch = new Stopwatch();

		overallStopwatch.Start();

		var expandoObject = CreateAndFillExpando(numValues);

		dynamicReadStopwatch.Start();

		TestDynamic(expandoObject, numValues);

		dynamicReadStopwatch.Stop();

		dictionaryReadStopwatch.Start();

		TestDictionary(expandoObject, numValues);

		dictionaryReadStopwatch.Stop();
		overallStopwatch.Stop();

		Console.WriteLine($"Dynamic Read Time: {dynamicReadStopwatch.ElapsedMilliseconds} ms");
		Console.WriteLine($"IDictionary<string, object> Read Time: {dictionaryReadStopwatch.ElapsedMilliseconds} ms");
		Console.WriteLine($"Overall Performance Time: {overallStopwatch.ElapsedMilliseconds} ms");
	}

	[TestCase(1)]
	[TestCase(5)]
	[TestCase(20)]
	[TestCase(100)]
	[TestCase(300)]
	[TestCase(1000)]
	public void DictionaryTest(int numValues)
	{
		var overallStopwatch = new Stopwatch();
		var dynamicReadStopwatch = new Stopwatch();
		var dictionaryReadStopwatch = new Stopwatch();

		overallStopwatch.Start();

		var dictionary = CreateAndFillDictionary(numValues);

		dynamicReadStopwatch.Start();

		TestDynamic(ToExpando(dictionary), numValues);

		dynamicReadStopwatch.Stop();

		dictionaryReadStopwatch.Start();

		TestDictionary(dictionary, numValues);

		dictionaryReadStopwatch.Stop();
		overallStopwatch.Stop();

		Console.WriteLine($"Dynamic Read Time: {dynamicReadStopwatch.ElapsedMilliseconds} ms");
		Console.WriteLine($"IDictionary<string, object> Read Time: {dictionaryReadStopwatch.ElapsedMilliseconds} ms");
		Console.WriteLine($"Overall Performance Time: {overallStopwatch.ElapsedMilliseconds} ms");
	}

	private static ExpandoObject ToExpando(IDictionary<string, object> dictionary)
	{
		var expando = new ExpandoObject();
		var expandoDict = (IDictionary<string, object>)expando;

		foreach (var kvp in dictionary)
			expandoDict[kvp.Key] = kvp.Value;

		return expando;
	}

	private void TestDynamic(dynamic list, int numValues)
	{
		for (var i = 0; i < numValues; i++)
		{
			string value = list.Key0;
			Trace.WriteLine(value);
		}
	}

	private void TestDictionary(IDictionary<string, object> list, int numValues)
	{
		for (var i = 0; i < numValues; i++)
		{
			var value = list[$"Key{i}"].ToString();
			Trace.WriteLine(value);
		}
	}

	private IDictionary<string, object> CreateAndFillExpando(int numValues)
	{
		var expandoDict = (IDictionary<string, object>)new ExpandoObject();

		for (var i = 0; i < numValues; i++)
			expandoDict[$"Key{i}"] = $"Value{i}";

		return expandoDict;
	}

	private IDictionary<string, object> CreateAndFillDictionary(int numValues)
	{
		var dictionary = new Dictionary<string, object>();

		for (var i = 0; i < numValues; i++)
			dictionary[$"Key{i}"] = $"Value{i}";

		return dictionary;
	}
}