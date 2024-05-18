using NUnit.Framework;

namespace Simplify.Web.Tests.Controllers.V1.Matcher;

[TestFixture]
public class Controller1PathParserTests
{
	// TODO
	// private Controller1PathParser _parser = null!;

	// [SetUp]
	// public void Initialize() => _parser = new Controller1PathParser();

	// [Test]
	// public void Parse_Root_NoSegments()
	// {
	// 	// Act
	// 	var result = _parser.Parse("/");

	// 	// Assert
	// 	Assert.That(result.Items.Count, Is.EqualTo(0));
	// }

	// [Test]
	// public void Parse_OneSegment_OneSegment()
	// {
	// 	// Act
	// 	var result = _parser.Parse("/foo");

	// 	// Assert

	// 	Assert.That(result.Items.Count, Is.EqualTo(1));
	// 	Assert.That(result.Items[0] as PathSegment, Is.Not.Null);
	// 	Assert.That(result.Items[0].Name, Is.EqualTo("foo"));
	// }

	// [Test]
	// public void Parse_MultipleSegments_MultipleSegments()
	// {
	// 	// Act
	// 	var result = _parser.Parse("/foo/bar/test");

	// 	// Assert

	// 	Assert.That(result.Items.Count, Is.EqualTo(3));
	// 	Assert.That(result.Items[0] as PathSegment, Is.Not.Null);
	// 	Assert.That(result.Items[0].Name, Is.EqualTo("foo"));
	// 	Assert.That(result.Items[1] as PathSegment, Is.Not.Null);
	// 	Assert.That(result.Items[1].Name, Is.EqualTo("bar"));
	// 	Assert.That(result.Items[2] as PathSegment, Is.Not.Null);
	// 	Assert.That(result.Items[2].Name, Is.EqualTo("test"));
	// }

	// [Test]
	// public void Parse_OneSegmentOneParameter_OneSegmentOneParameter()
	// {
	// 	// Act
	// 	var result = _parser.Parse("/foo/{name}");

	// 	// Assert

	// 	Assert.That(result.Items.Count, Is.EqualTo(2));
	// 	Assert.That(result.Items[0] as PathSegment, Is.Not.Null);
	// 	Assert.That(result.Items[0].Name, Is.EqualTo("foo"));

	// 	Assert.That(result.Items[1] as PathParameter, Is.Not.Null);
	// 	Assert.That(result.Items[1].Name, Is.EqualTo("name"));
	// 	Assert.That(((PathParameter)result.Items[1]).Type, Is.EqualTo(typeof(string)));
	// }

	// [Test]
	// public void Parse_OneSegmentTwoParameters_OneSegmentTwoParameters()
	// {
	// 	// Act
	// 	var result = _parser.Parse("/foo/{name}/{id:int}");

	// 	// Assert

	// 	Assert.That(result.Items.Count, Is.EqualTo(3));
	// 	Assert.That(result.Items[0] as PathSegment, Is.Not.Null);
	// 	Assert.That(result.Items[0].Name, Is.EqualTo("foo"));

	// 	Assert.That(result.Items[1] as PathParameter, Is.Not.Null);
	// 	Assert.That(result.Items[1].Name, Is.EqualTo("name"));
	// 	Assert.That(((PathParameter)result.Items[1]).Type, Is.EqualTo(typeof(string)));

	// 	Assert.That(result.Items[2] as PathParameter, Is.Not.Null);
	// 	Assert.That(result.Items[2].Name, Is.EqualTo("id"));
	// 	Assert.That(((PathParameter)result.Items[2]).Type, Is.EqualTo(typeof(int)));
	// }

	// [Test]
	// public void Parse_BadParameters_ExceptionThrown()
	// {
	// 	// Act & Assert

	// 	Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/{id:int"));
	// 	Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/id:int}"));
	// 	Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/:"));
	// 	Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/{{"));
	// 	Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/}}"));
	// 	Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/:}"));
	// 	Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/{:"));
	// 	Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/{}"));
	// 	Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/{:}"));
	// 	Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/{{a:int}}"));
	// 	Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/]{a:int{"));
	// 	Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/]{@#$32127!&}"));
	// }

	// [Test]
	// public void Parse_UnrecognizedParameterType_ExceptionThrown()
	// {
	// 	// Act & Assert

	// 	Assert.Throws<ControllerRouteException>(() => _parser.Parse("/foo/{id:foo}"));
	// }

	// [Test]
	// public void Parse_TwoSegmentsOneParameterInTheMiddle_TwoSegmentsOneParameterInTheMiddle()
	// {
	// 	// Act
	// 	var result = _parser.Parse("/foo/{name}/bar");

	// 	// Assert

	// 	Assert.That(result.Items.Count, Is.EqualTo(3));
	// 	Assert.That(result.Items[0] as PathSegment, Is.Not.Null);
	// 	Assert.That(result.Items[0].Name, Is.EqualTo("foo"));

	// 	Assert.That(result.Items[1] as PathParameter, Is.Not.Null);
	// 	Assert.That(result.Items[1].Name, Is.EqualTo("name"));
	// 	Assert.That(((PathParameter)result.Items[1]).Type, Is.EqualTo(typeof(string)));

	// 	Assert.That(result.Items[2] as PathSegment, Is.Not.Null);
	// 	Assert.That(result.Items[2].Name, Is.EqualTo("bar"));
	// }

	// [Test]
	// public void Parse_DecimalParameter_Parsed()
	// {
	// 	// Act
	// 	var result = _parser.Parse("/{id:decimal}");

	// 	// Assert

	// 	Assert.That(result.Items.Count, Is.EqualTo(1));
	// 	Assert.That(result.Items[0] as PathParameter, Is.Not.Null);
	// 	Assert.That(result.Items[0].Name, Is.EqualTo("id"));
	// 	Assert.That(((PathParameter)result.Items[0]).Type, Is.EqualTo(typeof(decimal)));
	// }

	// [Test]
	// public void Parse_BoolParameter_Parsed()
	// {
	// 	// Act
	// 	var result = _parser.Parse("/{foo:bool}");

	// 	// Assert

	// 	Assert.That(result.Items.Count, Is.EqualTo(1));
	// 	Assert.That(result.Items[0] as PathParameter, Is.Not.Null);
	// 	Assert.That(result.Items[0].Name, Is.EqualTo("foo"));
	// 	Assert.That(((PathParameter)result.Items[0]).Type, Is.EqualTo(typeof(bool)));
	// }

	// [Test]
	// public void Parse_StringArrayShortVersionParameter_Parsed()
	// {
	// 	// Act
	// 	var result = _parser.Parse("/{stringArray:[]}");

	// 	// Assert

	// 	Assert.That(result.Items.Count, Is.EqualTo(1));
	// 	Assert.That(result.Items[0] as PathParameter, Is.Not.Null);
	// 	Assert.That(result.Items[0].Name, Is.EqualTo("stringArray"));
	// 	Assert.That(((PathParameter)result.Items[0]).Type, Is.EqualTo(typeof(string[])));
	// }

	// [Test]
	// public void Parse_StringArrayParameter_Parsed()
	// {
	// 	// Act
	// 	var result = _parser.Parse("/{stringArray:string[]}");

	// 	// Assert

	// 	Assert.That(result.Items.Count, Is.EqualTo(1));
	// 	Assert.That(result.Items[0] as PathParameter, Is.Not.Null);
	// 	Assert.That(result.Items[0].Name, Is.EqualTo("stringArray"));
	// 	Assert.That(((PathParameter)result.Items[0]).Type, Is.EqualTo(typeof(string[])));
	// }

	// [Test]
	// public void Parse_IntArrayParameter_Parsed()
	// {
	// 	// Act
	// 	var result = _parser.Parse("/{intArray:int[]}");

	// 	// Assert

	// 	Assert.That(result.Items.Count, Is.EqualTo(1));
	// 	Assert.That(result.Items[0] as PathParameter, Is.Not.Null);
	// 	Assert.That(result.Items[0].Name, Is.EqualTo("intArray"));
	// 	Assert.That(((PathParameter)result.Items[0]).Type, Is.EqualTo(typeof(int[])));
	// }

	// [Test]
	// public void Parse_DecimalArrayParameter_Parsed()
	// {
	// 	// Act
	// 	var result = _parser.Parse("/{decimalArray:decimal[]}");

	// 	// Assert

	// 	Assert.That(result.Items.Count, Is.EqualTo(1));
	// 	Assert.That(result.Items[0] as PathParameter, Is.Not.Null);
	// 	Assert.That(result.Items[0].Name, Is.EqualTo("decimalArray"));
	// 	Assert.That(((PathParameter)result.Items[0]).Type, Is.EqualTo(typeof(decimal[])));
	// }

	// [Test]
	// public void Parse_privateBoolArrayParameter_Parsed()
	// {
	// 	// Act
	// 	var result = _parser.Parse("/{boolArray:bool[]}");

	// 	// Assert

	// 	Assert.That(result.Items.Count, Is.EqualTo(1));
	// 	Assert.That(result.Items[0] as PathParameter, Is.Not.Null);
	// 	Assert.That(result.Items[0].Name, Is.EqualTo("boolArray"));
	// 	Assert.That(((PathParameter)result.Items[0]).Type, Is.EqualTo(typeof(bool[])));
	// }
}