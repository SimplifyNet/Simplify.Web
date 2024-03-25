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
	public void IsTypeValidForParsing_IntList_True()
	{
		Assert.IsTrue(ArrayToSpecifiedListParser.IsTypeValidForParsing(typeof(IList<int>)));
	}

	[Test]
	public void IsTypeValidForParsing_EnumList_True()
	{
		Assert.IsTrue(ArrayToSpecifiedListParser.IsTypeValidForParsing(typeof(IList<TestEnum>)));
	}

	[Test]
	public void IsTypeValidForParsing_UndefinedType_False()
	{
		Assert.IsFalse(ArrayToSpecifiedListParser.IsTypeValidForParsing(typeof(TestController1)));
	}

	[Test]
	public void IsTypeValidForParsing_UndefinedGenericType_False()
	{
		Assert.Throws<ModelNotSupportedException>(() => ArrayToSpecifiedListParser.IsTypeValidForParsing(typeof(IList<TestController1>)));
	}

	[Test]
	public void IsTypeValidForParsing_StringArray_False()
	{
		Assert.IsFalse(ArrayToSpecifiedListParser.IsTypeValidForParsing(typeof(string[])));
	}

	[Test]
	public void ParseUndefined_EnumList_ParsedCorrectly()
	{
		// Act
		var result = (IList<TestEnum>)ArrayToSpecifiedListParser.ParseUndefined(new[] { "2", "1" }, typeof(IList<TestEnum>))!;

		// Assert

		Assert.AreEqual(TestEnum.Value2, result[0]);
		Assert.AreEqual(TestEnum.Value1, result[1]);
	}

	[Test]
	public void ParseUndefined_StringsList_Null()
	{
		// Act
		var result = (string[])ArrayToSpecifiedListParser.ParseUndefined(new[] { "val1", "val2" }, typeof(string[]))!;

		// Assert

		Assert.IsNull(result);
	}
}