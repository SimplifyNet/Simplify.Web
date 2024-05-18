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

	[Test]
	public void Parse_DecimalParameter_Parsed()
	{
		// Act
		var result = Controller1PathParser.Parse("/{id:decimal}");

		// Assert

		Assert.That(result.Count, Is.EqualTo(1));
		Assert.That(result[0] as PathParameter, Is.Not.Null);
		Assert.That(result[0].Name, Is.EqualTo("id"));
		Assert.That(((PathParameter)result[0]).Type, Is.EqualTo(typeof(decimal)));
	}

	[Test]
	public void Parse_BoolParameter_Parsed()
	{
		// Act
		var result = Controller1PathParser.Parse("/{foo:bool}");

		// Assert

		Assert.That(result.Count, Is.EqualTo(1));
		Assert.That(result[0] as PathParameter, Is.Not.Null);
		Assert.That(result[0].Name, Is.EqualTo("foo"));
		Assert.That(((PathParameter)result[0]).Type, Is.EqualTo(typeof(bool)));
	}

	[Test]
	public void Parse_StringArrayShortVersionParameter_Parsed()
	{
		// Act
		var result = Controller1PathParser.Parse("/{stringArray:[]}");

		// Assert

		Assert.That(result.Count, Is.EqualTo(1));
		Assert.That(result[0] as PathParameter, Is.Not.Null);
		Assert.That(result[0].Name, Is.EqualTo("stringArray"));
		Assert.That(((PathParameter)result[0]).Type, Is.EqualTo(typeof(string[])));
	}

	[Test]
	public void Parse_StringArrayParameter_Parsed()
	{
		// Act
		var result = Controller1PathParser.Parse("/{stringArray:string[]}");

		// Assert

		Assert.That(result.Count, Is.EqualTo(1));
		Assert.That(result[0] as PathParameter, Is.Not.Null);
		Assert.That(result[0].Name, Is.EqualTo("stringArray"));
		Assert.That(((PathParameter)result[0]).Type, Is.EqualTo(typeof(string[])));
	}

	[Test]
	public void Parse_IntArrayParameter_Parsed()
	{
		// Act
		var result = Controller1PathParser.Parse("/{intArray:int[]}");

		// Assert

		Assert.That(result.Count, Is.EqualTo(1));
		Assert.That(result[0] as PathParameter, Is.Not.Null);
		Assert.That(result[0].Name, Is.EqualTo("intArray"));
		Assert.That(((PathParameter)result[0]).Type, Is.EqualTo(typeof(int[])));
	}

	[Test]
	public void Parse_DecimalArrayParameter_Parsed()
	{
		// Act
		var result = Controller1PathParser.Parse("/{decimalArray:decimal[]}");

		// Assert

		Assert.That(result.Count, Is.EqualTo(1));
		Assert.That(result[0] as PathParameter, Is.Not.Null);
		Assert.That(result[0].Name, Is.EqualTo("decimalArray"));
		Assert.That(((PathParameter)result[0]).Type, Is.EqualTo(typeof(decimal[])));
	}

	[Test]
	public void Parse_privateBoolArrayParameter_Parsed()
	{
		// Act
		var result = Controller1PathParser.Parse("/{boolArray:bool[]}");

		// Assert

		Assert.That(result.Count, Is.EqualTo(1));
		Assert.That(result[0] as PathParameter, Is.Not.Null);
		Assert.That(result[0].Name, Is.EqualTo("boolArray"));
		Assert.That(((PathParameter)result[0]).Type, Is.EqualTo(typeof(bool[])));
	}
}