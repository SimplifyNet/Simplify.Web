using System.Collections.Generic;
using NUnit.Framework;
using Simplify.Web.Model.Binding;
using Simplify.Web.Model.Binding.Parsers;
using Simplify.Web.Tests.Model.Binding.Parsers.TestTypes;

namespace Simplify.Web.Tests.Model.Binding.Parsers;

[TestFixture]
public class ArrayToSpecifiedListParserTests
{
	[Test]
	public void IsTypeValidForParsing_IntList_True() =>
		Assert.That(ArrayToSpecifiedListParser.IsTypeValidForParsing(typeof(IList<int>)), Is.True);

	[Test]
	public void IsTypeValidForParsing_EnumList_True() =>
		Assert.That(ArrayToSpecifiedListParser.IsTypeValidForParsing(typeof(IList<TestEnum>)), Is.True);

	[Test]
	public void IsTypeValidForParsing_UndefinedType_False() =>
		Assert.That(ArrayToSpecifiedListParser.IsTypeValidForParsing(typeof(TestController1)), Is.False);

	[Test]
	public void IsTypeValidForParsing_UndefinedGenericType_False() =>
		Assert.Throws<ModelNotSupportedException>(() => ArrayToSpecifiedListParser.IsTypeValidForParsing(typeof(IList<TestController1>)));

	[Test]
	public void IsTypeValidForParsing_StringArray_False() =>
		Assert.That(ArrayToSpecifiedListParser.IsTypeValidForParsing(typeof(string[])), Is.False);

	[Test]
	public void ParseUndefined_EnumList_ParsedCorrectly()
	{
		// Act
		var result = (IList<TestEnum>)ArrayToSpecifiedListParser.ParseUndefined(["2", "1"], typeof(IList<TestEnum>))!;

		// Assert

		Assert.That(result[0], Is.EqualTo(TestEnum.Value2));
		Assert.That(result[1], Is.EqualTo(TestEnum.Value1));
	}

	[Test]
	public void ParseUndefined_StringsList_Null()
	{
		// Act
		var result = (string[])ArrayToSpecifiedListParser.ParseUndefined(["val1", "val2"], typeof(string[]))!;

		// Assert

		Assert.That(result, Is.Null);
	}
}