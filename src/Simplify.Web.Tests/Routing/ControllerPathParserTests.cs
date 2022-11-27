using NUnit.Framework;
using Simplify.Web.Routing;

namespace Simplify.Web.Tests.Routing;

[TestFixture]
public class ControllerPathParserTests
{
	private ControllerPathParser _parser = null!;

	[SetUp]
	public void Initialize() => _parser = new ControllerPathParser();

	[Test]
	public void Parse_Root_NoSegments()
	{
		// Act
		var result = _parser.Parse("/");

		// Assert
		Assert.AreEqual(0, result.Items.Count);
	}

	[Test]
	public void Parse_OneSegment_OneSegment()
	{
		// Act
		var result = _parser.Parse("/foo");

		// Assert

		Assert.AreEqual(1, result.Items.Count);
		Assert.IsNotNull(result.Items[0] as PathSegment);
		Assert.AreEqual("foo", result.Items[0].Name);
	}

	[Test]
	public void Parse_MultipleSegments_MultipleSegments()
	{
		// Act
		var result = _parser.Parse("/foo/bar/test");

		// Assert

		Assert.AreEqual(3, result.Items.Count);
		Assert.IsNotNull(result.Items[0] as PathSegment);
		Assert.AreEqual("foo", result.Items[0].Name);
		Assert.IsNotNull(result.Items[1] as PathSegment);
		Assert.AreEqual("bar", result.Items[1].Name);
		Assert.IsNotNull(result.Items[2] as PathSegment);
		Assert.AreEqual("test", result.Items[2].Name);
	}

	[Test]
	public void Parse_OneSegmentOneParameter_OneSegmentOneParameter()
	{
		// Act
		var result = _parser.Parse("/foo/{name}");

		// Assert

		Assert.AreEqual(2, result.Items.Count);
		Assert.IsNotNull(result.Items[0] as PathSegment);
		Assert.AreEqual("foo", result.Items[0].Name);

		Assert.IsNotNull(result.Items[1] as PathParameter);
		Assert.AreEqual("name", result.Items[1].Name);
		Assert.AreEqual(typeof(string), ((PathParameter)result.Items[1]).Type);
	}

	[Test]
	public void Parse_OneSegmentTwoParameters_OneSegmentTwoParameters()
	{
		// Act
		var result = _parser.Parse("/foo/{name}/{id:int}");

		// Assert

		Assert.AreEqual(3, result.Items.Count);
		Assert.IsNotNull(result.Items[0] as PathSegment);
		Assert.AreEqual("foo", result.Items[0].Name);

		Assert.IsNotNull(result.Items[1] as PathParameter);
		Assert.AreEqual("name", result.Items[1].Name);
		Assert.AreEqual(typeof(string), ((PathParameter)result.Items[1]).Type);

		Assert.IsNotNull(result.Items[2] as PathParameter);
		Assert.AreEqual("id", result.Items[2].Name);
		Assert.AreEqual(typeof(int), ((PathParameter)result.Items[2]).Type);
	}

	[Test]
	public void Parse_BadParameters_ExceptionThrown()
	{
		// Act & Assert

		Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/{id:int"));
		Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/id:int}"));
		Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/:"));
		Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/{{"));
		Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/}}"));
		Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/:}"));
		Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/{:"));
		Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/{}"));
		Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/{:}"));
		Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/{{a:int}}"));
		Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/]{a:int{"));
		Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/]{@#$32127!&}"));
	}

	[Test]
	public void Parse_UnrecognizedParameterType_ExceptionThrown()
	{
		// Act & Assert

		Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/{id:foo}"));
	}

	[Test]
	public void Parse_TwoSegmentsOneParameterInTheMiddle_TwoSegmentsOneParameterInTheMiddle()
	{
		// Act
		var result = _parser.Parse("/foo/{name}/bar");

		// Assert

		Assert.AreEqual(3, result.Items.Count);
		Assert.IsNotNull(result.Items[0] as PathSegment);
		Assert.AreEqual("foo", result.Items[0].Name);

		Assert.IsNotNull(result.Items[1] as PathParameter);
		Assert.AreEqual("name", result.Items[1].Name);
		Assert.AreEqual(typeof(string), ((PathParameter)result.Items[1]).Type);

		Assert.IsNotNull(result.Items[2] as PathSegment);
		Assert.AreEqual("bar", result.Items[2].Name);
	}

	[Test]
	public void Parse_DecimalParameter_Parsed()
	{
		// Act
		var result = _parser.Parse("/{id:decimal}");

		// Assert

		Assert.AreEqual(1, result.Items.Count);
		Assert.IsNotNull(result.Items[0] as PathParameter);
		Assert.AreEqual("id", result.Items[0].Name);
		Assert.AreEqual(typeof(decimal), ((PathParameter)result.Items[0]).Type);
	}

	[Test]
	public void Parse_BoolParameter_Parsed()
	{
		// Act
		var result = _parser.Parse("/{foo:bool}");

		// Assert

		Assert.AreEqual(1, result.Items.Count);
		Assert.IsNotNull(result.Items[0] as PathParameter);
		Assert.AreEqual("foo", result.Items[0].Name);
		Assert.AreEqual(typeof(bool), ((PathParameter)result.Items[0]).Type);
	}

	[Test]
	public void Parse_StringArrayShortVersionParameter_Parsed()
	{
		// Act
		var result = _parser.Parse("/{stringArray:[]}");

		// Assert

		Assert.AreEqual(1, result.Items.Count);
		Assert.IsNotNull(result.Items[0] as PathParameter);
		Assert.AreEqual("stringArray", result.Items[0].Name);
		Assert.AreEqual(typeof(string[]), ((PathParameter)result.Items[0]).Type);
	}

	[Test]
	public void Parse_StringArrayParameter_Parsed()
	{
		// Act
		var result = _parser.Parse("/{stringArray:string[]}");

		// Assert

		Assert.AreEqual(1, result.Items.Count);
		Assert.IsNotNull(result.Items[0] as PathParameter);
		Assert.AreEqual("stringArray", result.Items[0].Name);
		Assert.AreEqual(typeof(string[]), ((PathParameter)result.Items[0]).Type);
	}

	[Test]
	public void Parse_IntArrayParameter_Parsed()
	{
		// Act
		var result = _parser.Parse("/{intArray:int[]}");

		// Assert

		Assert.AreEqual(1, result.Items.Count);
		Assert.IsNotNull(result.Items[0] as PathParameter);
		Assert.AreEqual("intArray", result.Items[0].Name);
		Assert.AreEqual(typeof(int[]), ((PathParameter)result.Items[0]).Type);
	}

	[Test]
	public void Parse_DecimalArrayParameter_Parsed()
	{
		// Act
		var result = _parser.Parse("/{decimalArray:decimal[]}");

		// Assert

		Assert.AreEqual(1, result.Items.Count);
		Assert.IsNotNull(result.Items[0] as PathParameter);
		Assert.AreEqual("decimalArray", result.Items[0].Name);
		Assert.AreEqual(typeof(decimal[]), ((PathParameter)result.Items[0]).Type);
	}

	[Test]
	public void Parse_privateBoolArrayParameter_Parsed()
	{
		// Act
		var result = _parser.Parse("/{boolArray:bool[]}");

		// Assert

		Assert.AreEqual(1, result.Items.Count);
		Assert.IsNotNull(result.Items[0] as PathParameter);
		Assert.AreEqual("boolArray", result.Items[0].Name);
		Assert.AreEqual(typeof(bool[]), ((PathParameter)result.Items[0]).Type);
	}
}