using System;
using System.Collections.Generic;
using NUnit.Framework;
using Simplify.Web.Controllers.Meta.Routing;
using Simplify.Web.Controllers.V2.Routing;

namespace Simplify.Web.Tests.Controllers.V2.Routing;

[TestFixture]
public class Controller2PathParserTests
{
	[Test]
	public void Parse_RootPath_NoSegments()
	{
		// Arrange

		const string controllerPath = "/";
		var invokeMethodParameters = new Dictionary<string, Type>();

		// Act
		var result = Controller2PathParser.Parse(controllerPath, invokeMethodParameters);

		// Assert
		Assert.That(result.Count, Is.EqualTo(0));
	}

	[Test]
	public void Parse_OneSegment_OneSegment()
	{
		// Arrange

		const string controllerPath = "/foo";
		var invokeMethodParameters = new Dictionary<string, Type>();

		var expectedPath = new PathItem[]
		{
			new PathSegment("foo")
		};

		// Act
		var result = Controller2PathParser.Parse(controllerPath, invokeMethodParameters);

		// Assert
		TestPathMatching(expectedPath, result);
	}

	[Test]
	public void Parse_OneSegmentOneParameter_OneSegmentOneParameter()
	{
		// Arrange

		const string controllerPath = "/foo/{bar}";

		var invokeMethodParameters = new Dictionary<string, Type>
		{
			{ "bar", typeof(string) }
		};

		var expectedPath = new PathItem[]
		{
			new PathSegment("foo"),
			new PathParameter("bar", typeof(string))
		};

		// Act
		var result = Controller2PathParser.Parse(controllerPath, invokeMethodParameters);

		// Assert
		TestPathMatching(expectedPath, result);
	}

	[Test]
	public void Parse_OneParameterInRouteNoParametersInMethod_PathParameterWillBeString()
	{
		// Arrange

		const string controllerPath = "/{foo}";

		var invokeMethodParameters = new Dictionary<string, Type>();

		var expectedPath = new PathItem[]
		{
			new PathParameter("foo", typeof(string))
		};

		// Act
		var result = Controller2PathParser.Parse(controllerPath, invokeMethodParameters);

		// Assert
		TestPathMatching(expectedPath, result);
	}

	[Test]
	public void Parse_NoParametersInRouteOneParameterInMethod_ControllerRouteException()
	{
		// Arrange

		const string controllerPath = "/foo";

		var invokeMethodParameters = new Dictionary<string, Type>
		{
			{ "bar", typeof(string) }
		};

		// Act & Assert
		Assert.Throws<ControllerRouteException>(() => Controller2PathParser.Parse(controllerPath, invokeMethodParameters));
	}

	[Test]
	public void Parse_OneSegmentTwoParameters_OneSegmentTwoParameters()
	{
		// Arrange

		const string controllerPath = "/foo/{bar}/{id}";

		var invokeMethodParameters = new Dictionary<string, Type>
		{
			{ "bar", typeof(string) },
			{ "id", typeof(int) }
		};

		var expectedPath = new PathItem[]
		{
			new PathSegment("foo"),
			new PathParameter("bar", typeof(string)),
			new PathParameter("id", typeof(int))
		};

		// Act
		var result = Controller2PathParser.Parse(controllerPath, invokeMethodParameters);

		// Assert
		TestPathMatching(expectedPath, result);
	}

	[TestCase("foo", "/{foo}", typeof(string))]
	[TestCase("foo", "/{Foo}", typeof(string))]
	public void Parse_OneParameter_Parsed(string name, string controllerPath, Type type)
	{
		// Arrange
		var expectedPath = new PathItem[]
		{
			new PathParameter(name, type)
		};

		var invokeMethodParameters = new Dictionary<string, Type>
		{
			{ name, type },
		};

		// Act
		var result = Controller2PathParser.Parse(controllerPath, invokeMethodParameters);

		// Assert
		TestPathMatching(expectedPath, result);
	}

	[Test]
	public void Parse_TwoSegmentsOneParameterInTheMiddle_TwoSegmentsOneParameterInTheMiddle()
	{
		// Arrange

		const string controllerPath = "/foo/{name}/bar";

		var invokeMethodParameters = new Dictionary<string, Type>
		{
			{ "name", typeof(string)},
		};

		var expectedPath = new PathItem[]
		{
			new PathSegment("foo"),
			new PathParameter("name", typeof(string)),
			new PathSegment("bar")
		};

		// Act
		var result = Controller2PathParser.Parse(controllerPath, invokeMethodParameters);

		// Assert
		TestPathMatching(expectedPath, result);
	}

	[Test]
	public void Parse_MultipleSegments_MultipleSegments()
	{
		// Arrange

		const string controllerPath = "/foo/bar/test";
		var invokeMethodParameters = new Dictionary<string, Type>();

		var expectedPath = new PathItem[]
		{
			new PathSegment("foo"),
			new PathSegment("bar"),
			new PathSegment("test")
		};

		// Act
		var result = Controller2PathParser.Parse(controllerPath, invokeMethodParameters);

		// Assert
		TestPathMatching(expectedPath, result);
	}

	[TestCase("/foo/{id:int}")]
	[TestCase("/foo/{{")]
	[TestCase("/foo/}}")]
	[TestCase("/foo/{}")]
	[TestCase("/foo/{:}")]
	[TestCase("/foo/{{a:int}}")]
	[TestCase("/foo/]{a:int{")]
	[TestCase("/foo/]{@#$32127!&}")]
	public void Parse_BadParameter_ExceptionThrown(string path) =>
		// Act & Assert
		Assert.Throws<ControllerRouteException>(() => Controller2PathParser.Parse(path, new Dictionary<string, Type>()));

	[Test]
	public void Parse_UnrecognizedParameterType_ExceptionThrown() =>
		// Act & Assert
		Assert.Throws<ControllerRouteException>(() => Controller2PathParser.Parse("/foo/{id:foo}", new Dictionary<string, Type>()));

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