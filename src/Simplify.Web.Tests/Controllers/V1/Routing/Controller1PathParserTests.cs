using System;
using System.Collections.Generic;
using NUnit.Framework;
using Simplify.Web.Controllers.Meta.Routing;
using Simplify.Web.Controllers.V1.Routing;

namespace Simplify.Web.Tests.Controllers.V1.Routing;

[TestFixture]
public class Controller1PathParserTests
{
	[Test]
	public void Parse_RootPath_NoSegments()
	{
		// Act
		var result = Controller1PathParser.Parse("/");

		// Assert
		Assert.That(result.Count, Is.EqualTo(0));
	}

	[Test]
	public void Parse_OneSegment_OneSegment()
	{
		// Arrange
		var expectedPath = new PathItem[]
		{
			new PathSegment("foo")
		};

		// Act
		var result = Controller1PathParser.Parse("/foo");

		// Assert
		TestPathMatching(expectedPath, result);
	}

	[Test]
	public void Parse_OneSegmentOneParameter_OneSegmentOneParameter()
	{
		// Arrange
		var expectedPath = new PathItem[]
		{
			new PathSegment("foo"),
			new PathParameter("bar", typeof(string))
		};

		// Act
		var result = Controller1PathParser.Parse("/foo/{bar}");

		// Assert
		TestPathMatching(expectedPath, result);
	}

	[Test]
	public void Parse_OneSegmentTwoParameters_OneSegmentTwoParameters()
	{
		// Arrange
		var expectedPath = new PathItem[]
		{
			new PathSegment("foo"),
			new PathParameter("bar", typeof(string)),
			new PathParameter("id", typeof(int))
		};

		// Act
		var result = Controller1PathParser.Parse("/foo/{bar}/{id:int}");

		// Assert
		TestPathMatching(expectedPath, result);
	}

	[TestCase("id", "/{id:decimal}", typeof(decimal))]
	[TestCase("foo", "/{foo:bool}", typeof(bool))]
	[TestCase("stringArray", "/{stringArray:[]}", typeof(string[]))]
	[TestCase("stringArray", "/{stringArray:string[]}", typeof(string[]))]
	[TestCase("intArray", "/{intArray:int[]}", typeof(int[]))]
	[TestCase("decimalArray", "/{decimalArray:decimal[]}", typeof(decimal[]))]
	[TestCase("boolArray", "/{boolArray:bool[]}", typeof(bool[]))]
	public void Parse_OneParameter_Parsed(string name, string controllerPath, Type type)
	{
		// Arrange
		var expectedPath = new PathItem[]
		{
			new PathParameter(name, type)
		};

		// Act
		var result = Controller1PathParser.Parse(controllerPath);

		// Assert
		TestPathMatching(expectedPath, result);
	}

	[Test]
	public void Parse_TwoSegmentsOneParameterInTheMiddle_TwoSegmentsOneParameterInTheMiddle()
	{
		// Arrange
		var expectedPath = new PathItem[]
		{
			new PathSegment("foo"),
			new PathParameter("name", typeof(string)),
			new PathSegment("bar")
		};

		// Act
		var result = Controller1PathParser.Parse("/foo/{name}/bar");

		// Assert
		TestPathMatching(expectedPath, result);
	}

	[Test]
	public void Parse_MultipleSegments_MultipleSegments()
	{
		// Arrange
		var expectedPath = new PathItem[]
		{
			new PathSegment("foo"),
			new PathSegment("bar"),
			new PathSegment("test")
		};

		// Act
		var result = Controller1PathParser.Parse("/foo/bar/test");

		// Assert
		TestPathMatching(expectedPath, result);
	}

	[TestCase("/foo/{id:int")]
	[TestCase("/foo/id:int}")]
	[TestCase("/foo/:")]
	[TestCase("/foo/{{")]
	[TestCase("/foo/}}")]
	[TestCase("/foo/:}")]
	[TestCase("/foo/{:")]
	[TestCase("/foo/{}")]
	[TestCase("/foo/{:}")]
	[TestCase("/foo/{{a:int}}")]
	[TestCase("/foo/]{a:int{")]
	[TestCase("/foo/]{@#$32127!&}")]
	public void Parse_BadParameter_ExceptionThrown(string path) =>
		// Act & Assert
		Assert.Throws<ControllerRouteException>(() => Controller1PathParser.Parse(path));

	[Test]
	public void Parse_UnrecognizedParameterType_ExceptionThrown() =>
		// Act & Assert
		Assert.Throws<ControllerRouteException>(() => Controller1PathParser.Parse("/foo/{id:foo}"));

	private static void TestPathMatching(IList<PathItem> expectedPath, IList<PathItem> actualPath)
	{
		Assert.That(expectedPath.Count, Is.EqualTo(actualPath.Count));

		for (var i = 0; i < expectedPath.Count; i++)
		{
			Assert.That(expectedPath[i].GetType(), Is.EqualTo(actualPath[i].GetType()));
			Assert.That(expectedPath[i].Name, Is.EqualTo(actualPath[i].Name));

			if (expectedPath[i] is not PathParameter)
				continue;

			var expectedPathParameter = (PathParameter)expectedPath[i];
			var actualPathParameter = (PathParameter)expectedPath[i];

			Assert.That(expectedPathParameter.Type, Is.EqualTo(actualPathParameter.Type));
		}
	}
}