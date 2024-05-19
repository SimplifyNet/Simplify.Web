using System;
using NUnit.Framework;
using Simplify.Web.System;

namespace Simplify.Web.Controllers.V1.Routing;

[TestFixture]
public class StringConverterTests
{
	[TestCase(typeof(string), "foo", "foo")]
	[TestCase(typeof(int), "1", 1)]
	[TestCase(typeof(long), "1", 1L)]
	[TestCase(typeof(float), "1", 1f)]
	[TestCase(typeof(double), "1", 1d)]
	[TestCase(typeof(bool), "true", true)]
	public void Convert_SpecifiedValue_ConvertedToDestinationType(Type destinationType, string sourceValue, object expectedValue)
	{
		// Act
		var result = StringConverter.TryConvert(destinationType, sourceValue);

		// Assert

		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.EqualTo(expectedValue));
	}

	[Test]
	public void Convert_Decimal_Converted() =>
		Convert_SpecifiedValue_ConvertedToDestinationType(typeof(decimal), "1", 1m);

	[Test]
	public void Convert_StringArray_Converted() =>
		Convert_SpecifiedValue_ConvertedToDestinationType(typeof(string[]), "a,b,c", new string[] { "a", "b", "c" });

	[Test]
	public void Convert_IntArray_Converted() =>
		Convert_SpecifiedValue_ConvertedToDestinationType(typeof(int[]), "1,2,3", new int[] { 1, 2, 3 });

	[Test]
	public void Convert_DecimalArray_Converted() =>
		Convert_SpecifiedValue_ConvertedToDestinationType(typeof(decimal[]), "1,2,3", new decimal[] { 1m, 2m, 3m });

	[Test]
	public void Convert_BoolArray_Converted() =>
		Convert_SpecifiedValue_ConvertedToDestinationType(typeof(bool[]), "false,true,false", new bool[] { false, true, false });
}