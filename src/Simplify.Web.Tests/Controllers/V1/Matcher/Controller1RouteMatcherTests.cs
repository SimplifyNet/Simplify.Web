using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers.V1.Matcher;

namespace Simplify.Web.Tests.Routing;

[TestFixture]
public class Controller1RouteMatcherTests
{
	private Controller1RouteMatcher _matcher = null!;

	private Mock<IController1PathParser> _controllerPathParser = null!;

	[SetUp]
	public void Initialize()
	{
		_controllerPathParser = new Mock<IController1PathParser>();
		_matcher = new Controller1RouteMatcher(_controllerPathParser.Object);
	}

	[Test]
	public void Match_SourceEmptyOrNull_False()
	{
		// Act
		var result = _matcher.Match(null, "/test");
		var result2 = _matcher.Match("", "/test");

		// Assert
		Assert.That(result.Success, Is.False);
		Assert.That(result2.Success, Is.False);
	}

	[Test]
	public void Match_SingleSegmentWithNullString_True()
	{
		// Act
		var result = _matcher.Match("/test", null);

		// Assert
		Assert.That(result.Success, Is.True);
	}

	[Test]
	public void Match_RootWithRoot_True()
	{
		_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>())).Returns(new ControllerPath(new List<PathItem>()));

		// Act
		var result = _matcher.Match("/", "/");

		// Assert
		Assert.That(result.Success, Is.True);
	}

	[Test]
	public void Match_SingleSegments_True()
	{
		// Assign
		_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
			.Returns(new ControllerPath(new List<PathItem> { new PathSegment("foo") }));

		// Act
		var result = _matcher.Match("/foo", "/foo");

		// Assert
		Assert.That(result.Success, Is.True);
	}

	[Test]
	public void Match_SingleSegmentsNotMatching_False()
	{
		// Assign
		_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
			.Returns(new ControllerPath(new List<PathItem> { new PathSegment("bar") }));

		// Act
		var result = _matcher.Match("/foo", "/bar");

		// Assert
		Assert.That(result.Success, Is.False);
	}

	[Test]
	public void Match_MultipleSegmentsWithFirstMatchedSegment_False()
	{
		// Assign
		_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
			.Returns(new ControllerPath(new List<PathItem> { new PathSegment("foo") }));

		// Act
		var result = _matcher.Match("/foo/bar/test", "/foo");

		// Assert
		Assert.That(result.Success, Is.False);
	}

	[Test]
	public void Match_SingleSegmentsWithMultipleSegments_False()
	{
		// Assign
		_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
			.Returns(
				new ControllerPath(new List<PathItem> { new PathSegment("foo"), new PathSegment("bar"), new PathSegment("test") }));

		// Act
		var result = _matcher.Match("/foo", "/foo/bar/test");

		// Assert
		Assert.That(result.Success, Is.False);
	}

	[Test]
	public void Match_TwoSegmentsWithSegmentAndParameter_TrueValueParsed()
	{
		// Assign
		_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
			.Returns(new ControllerPath(new List<PathItem> { new PathSegment("user"), new PathParameter("userName", typeof(string)) }));

		// Act

		var result = _matcher.Match("/user/test-user", "/user/{userName}");
		var routeParameters = result.RouteParameters!;

		// Assert

		Assert.That(result.Success, Is.True);
		Assert.That(routeParameters["userName"], Is.EqualTo("test-user"));
	}

	[Test]
	public void Match_TwoSegmentsWithSegmentAndParameterNotMatched_False()
	{
		// Assign
		_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
			.Returns(new ControllerPath(new List<PathItem> { new PathSegment("bar"), new PathParameter("userName", typeof(string)) }));

		// Act
		var result = _matcher.Match("/user/test-user", "/bar/{userName}");

		// Assert

		Assert.That(result.Success, Is.False);
	}

	[Test]
	public void Match_TwoSegmentsWithOneSegmentAndTwoParameters_False()
	{
		// Assign
		_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
			.Returns(
				new ControllerPath(new List<PathItem>
				{
					new PathSegment("foo"),
					new PathParameter("test", typeof (string)),
					new PathParameter("userName", typeof (string))
				}));

		// Act
		var result = _matcher.Match("/user/test-user", "/foo/{test}/{userName}");

		// Assert
		Assert.That(result.Success, Is.False);
	}

	[Test]
	public void Match_OneSegmentWithOneParameter_True()
	{
		// Assign
		_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
			.Returns(new ControllerPath(new List<PathItem> { new PathParameter("userName", typeof(string)) }));

		// Act

		var result = _matcher.Match("/user", "/{userName}");
		var routeParameters = result.RouteParameters!;

		// Assert

		Assert.That(result.Success, Is.True);
		Assert.That(routeParameters["userName"], Is.EqualTo("user"));
	}

	[Test]
	public void Match_ParameterTypeMismatch_False()
	{
		// Assign
		_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
			.Returns(new ControllerPath(new List<PathItem> { new PathParameter("userName", typeof(int)) }));

		// Act
		var result = _matcher.Match("/foo", "/{id:int}");

		// Assert
		Assert.That(result.Success, Is.False);
	}

	[Test]
	public void Match_TwoSegmentsWithTwoParameters_TrueParsed()
	{
		// Assign
		_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
			.Returns(new ControllerPath(new List<PathItem> { new PathParameter("test", typeof(string)), new PathParameter("name", typeof(string)) }));

		// Act

		var result = _matcher.Match("/foo/bar", "/{test}/{name}");
		var routeParameters = result.RouteParameters!;

		// Assert

		Assert.That(result.Success, Is.True);
		Assert.That(routeParameters["test"], Is.EqualTo("foo"));
		Assert.That(routeParameters["name"], Is.EqualTo("bar"));
	}

	[Test]
	public void Match_OneSegmentWithOneIntegerParameter_True()
	{
		// Assign
		_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
			.Returns(new ControllerPath(new List<PathItem> { new PathParameter("id", typeof(int)) }));

		// Act

		var result = _matcher.Match("/15", "/{id}");
		var routeParameters = result.RouteParameters!;

		// Assert

		Assert.That(result.Success, Is.True);
		Assert.That(routeParameters["id"], Is.EqualTo(15));
	}

	[Test]
	public void Match_OneSegmentWithOneDecimalParameter_True()
	{
		// Assign
		_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
			.Returns(new ControllerPath(new List<PathItem> { new PathParameter("id", typeof(decimal)) }));

		// Act

		var result = _matcher.Match("/15", "/{id}");
		var routeParameters = result.RouteParameters!;

		// Assert

		Assert.That(result.Success, Is.True);
		Assert.That(routeParameters["id"], Is.EqualTo((decimal)15));
	}

	[Test]
	public void Match_BoolParameter_True()
	{
		// Assign
		_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
			.Returns(new ControllerPath(new List<PathItem> { new PathParameter("foo", typeof(bool)) }));

		// Act

		var result = _matcher.Match("/true", "/{foo:bool}");
		var routeParameters = result.RouteParameters!;

		// Assert

		Assert.That(result.Success, Is.True);
		Assert.That(routeParameters["foo"], Is.EqualTo(true));
	}

	[Test]
	public void Match_StringArrayShortVersionParameter_True()
	{
		// Assign
		_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
			.Returns(new ControllerPath(new List<PathItem> { new PathParameter("foo", typeof(string[])) }));

		// Act

		var result = _matcher.Match("/hello,world,test", "/{foo:[]}");
		var routeParameters = result.RouteParameters!;

		// Assert

		Assert.That(result.Success, Is.True);

		var items = (IList<string>)routeParameters["foo"];

		Assert.That(items.Count, Is.EqualTo(3));
		Assert.That(items[0], Is.EqualTo("hello"));
		Assert.That(items[1], Is.EqualTo("world"));
		Assert.That(items[2], Is.EqualTo("test"));
	}

	[Test]
	public void Match_IntArrayShortVersionParameter_True()
	{
		// Assign
		_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
			.Returns(new ControllerPath(new List<PathItem> { new PathParameter("foo", typeof(int[])) }));

		// Act

		var result = _matcher.Match("/1,2,3", "/{foo:int[]}");
		var routeParameters = result.RouteParameters!;

		// Assert

		Assert.That(result.Success, Is.True);

		var items = (IList<int>)routeParameters["foo"];

		Assert.That(items.Count, Is.EqualTo(3));
		Assert.That(items[0], Is.EqualTo(1));
		Assert.That(items[1], Is.EqualTo(2));
		Assert.That(items[2], Is.EqualTo(3));
	}

	[Test]
	public void Match_DecimalArrayShortVersionParameter_True()
	{
		// Assign
		_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
			.Returns(new ControllerPath(new List<PathItem> { new PathParameter("foo", typeof(decimal[])) }));

		// Act

		var result = _matcher.Match("/1,2,3", "/{foo:decimal[]}");
		var routeParameters = result.RouteParameters!;

		// Assert

		Assert.That(result.Success, Is.True);

		var items = (IList<decimal>)routeParameters["foo"];

		Assert.That(items.Count, Is.EqualTo(3));
		Assert.That(items[0], Is.EqualTo(1));
		Assert.That(items[1], Is.EqualTo(2));
		Assert.That(items[2], Is.EqualTo(3));
	}

	[Test]
	public void Match_BoolArrayShortVersionParameterWithTypeMismatch_True()
	{
		// Assign
		_controllerPathParser.Setup(x => x.Parse(It.IsAny<string>()))
			.Returns(new ControllerPath(new List<PathItem> { new PathParameter("foo", typeof(bool[])) }));

		// Act

		var result = _matcher.Match("/true,false,str", "/{foo:bool[]}");
		var routeParameters = result.RouteParameters!;

		// Assert

		Assert.That(result.Success, Is.True);

		var items = (IList<bool>)routeParameters["foo"];

		Assert.That(items.Count, Is.EqualTo(2));
		Assert.That(items[0], Is.EqualTo(true));
		Assert.That(items[1], Is.EqualTo(false));
	}
}