using System;
using NUnit.Framework;
using Simplify.Web.Controllers.Meta.Routing;
using Simplify.Web.Controllers.V1.Routing;

namespace Simplify.Web.Tests.Controllers.V1.Routing;

[TestFixture]
public class Controller1PathParserTests
{
	[Test]
	public void Parse_Root_NoSegments()
	{
		// Act
		var result = Controller1PathParser.Parse("/");

		// Assert
		Assert.That(result.Count, Is.EqualTo(0));
	}

	[Test]
	public void Parse_OneSegment_OneSegment()
	{
		// Act
		var result = Controller1PathParser.Parse("/foo");

		// Assert

		Assert.That(result.Count, Is.EqualTo(1));
		Assert.That(result[0] as PathSegment, Is.Not.Null);
		Assert.That(result[0].Name, Is.EqualTo("foo"));
	}

	[Test]
	public void Parse_MultipleSegments_MultipleSegments()
	{
		// Act
		var result = Controller1PathParser.Parse("/foo/bar/test");

		// Assert

		Assert.That(result.Count, Is.EqualTo(3));
		Assert.That(result[0] as PathSegment, Is.Not.Null);
		Assert.That(result[0].Name, Is.EqualTo("foo"));
		Assert.That(result[1] as PathSegment, Is.Not.Null);
		Assert.That(result[1].Name, Is.EqualTo("bar"));
		Assert.That(result[2] as PathSegment, Is.Not.Null);
		Assert.That(result[2].Name, Is.EqualTo("test"));
	}

	[Test]
	public void Parse_OneSegmentOneParameter_OneSegmentOneParameter()
	{
		// Act
		var result = Controller1PathParser.Parse("/foo/{name}");

		// Assert

		Assert.That(result.Count, Is.EqualTo(2));
		Assert.That(result[0] as PathSegment, Is.Not.Null);
		Assert.That(result[0].Name, Is.EqualTo("foo"));

		Assert.That(result[1] as PathParameter, Is.Not.Null);
		Assert.That(result[1].Name, Is.EqualTo("name"));
		Assert.That(((PathParameter)result[1]).Type, Is.EqualTo(typeof(string)));
	}

	[Test]
	public void Parse_OneSegmentTwoParameters_OneSegmentTwoParameters()
	{
		// Act
		var result = Controller1PathParser.Parse("/foo/{name}/{id:int}");

		// Assert

		Assert.That(result.Count, Is.EqualTo(3));
		Assert.That(result[0] as PathSegment, Is.Not.Null);
		Assert.That(result[0].Name, Is.EqualTo("foo"));

		Assert.That(result[1] as PathParameter, Is.Not.Null);
		Assert.That(result[1].Name, Is.EqualTo("name"));
		Assert.That(((PathParameter)result[1]).Type, Is.EqualTo(typeof(string)));

		Assert.That(result[2] as PathParameter, Is.Not.Null);
		Assert.That(result[2].Name, Is.EqualTo("id"));
		Assert.That(((PathParameter)result[2]).Type, Is.EqualTo(typeof(int)));
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
	public void Parse_BadParameters_ExceptionThrown(string path) =>
		// Act & Assert
		Assert.Throws<ControllerRouteException>(() => Controller1PathParser.Parse(path));

	[Test]
	public void Parse_UnrecognizedParameterType_ExceptionThrown()
	{
		// Act & Assert

		Assert.Throws<ControllerRouteException>(() => Controller1PathParser.Parse("/foo/{id:foo}"));
	}

	[Test]
	public void Parse_TwoSegmentsOneParameterInTheMiddle_TwoSegmentsOneParameterInTheMiddle()
	{
		// Act
		var result = Controller1PathParser.Parse("/foo/{name}/bar");

		// Assert

		Assert.That(result.Count, Is.EqualTo(3));
		Assert.That(result[0] as PathSegment, Is.Not.Null);
		Assert.That(result[0].Name, Is.EqualTo("foo"));

		Assert.That(result[1] as PathParameter, Is.Not.Null);
		Assert.That(result[1].Name, Is.EqualTo("name"));
		Assert.That(((PathParameter)result[1]).Type, Is.EqualTo(typeof(string)));

		Assert.That(result[2] as PathSegment, Is.Not.Null);
		Assert.That(result[2].Name, Is.EqualTo("bar"));
	}

	[TestCase("id", "/{id:decimal}", typeof(decimal))]
	[TestCase("foo", "/{foo:bool}", typeof(bool))]
	[TestCase("stringArray", "/{stringArray:[]}", typeof(string[]))]
	[TestCase("stringArray", "/{stringArray:string[]}", typeof(string[]))]
	[TestCase("intArray", "/{intArray:int[]}", typeof(int[]))]
	[TestCase("decimalArray", "/{decimalArray:decimal[]}", typeof(decimal[]))]
	[TestCase("boolArray", "/{boolArray:bool[]}", typeof(bool[]))]
	public void Parse_Parameter_Parsed(string name, string controllerPath, Type type)
	{
		// Act
		var result = Controller1PathParser.Parse(controllerPath);

		// Assert

		Assert.That(result.Count, Is.EqualTo(1));
		Assert.That(result[0] as PathParameter, Is.Not.Null);
		Assert.That(result[0].Name, Is.EqualTo(name));
		Assert.That(((PathParameter)result[0]).Type, Is.EqualTo(type));
	}
}