using System.Collections.Generic;
using NUnit.Framework;
using Simplify.Web.Controllers.V1.Routing;
using Simplify.Web.Http.RequestPath;

namespace Simplify.Web.Tests.Controllers.V1.Routing;

[TestFixture]
public class Controller1RouteMatcherTests
{
	private readonly Controller1RouteMatcher _matcher = new();

	[TestCase("/test", null)]
	[TestCase("/test", "")]
	[TestCase("/", "/")]
	[TestCase("/foo", "/foo")]
	public void Match_MatchingParts_True(string currentPath, string controllerRoute)
	{
		// Act
		var result = _matcher.Match(currentPath.GetSplitPath(), new Controller1Route(controllerRoute));

		// Assert
		Assert.That(result.Success, Is.True);
	}

	[TestCase(null, "/test")]
	[TestCase("", "/test")]
	[TestCase("/test", "/")]
	[TestCase("/foo", "/bar")]
	[TestCase("/foo/bar/test", "/foo")]
	[TestCase("/foo", "/foo/bar/test")]
	[TestCase("/user/test-user", "/bar/{userName}")]
	[TestCase("/user/test-user", "/foo/{test}/{userName}")]
	[TestCase("/foo", "/{id:int}")]
	public void Match_NotMatchingParts_False(string currentPath, string controllerRoute)
	{
		// Act
		var result = _matcher.Match(currentPath.GetSplitPath(), new Controller1Route(controllerRoute));

		// Assert
		Assert.That(result.Success, Is.False);
	}

	[TestCase("/user/test-user", "/user/{userName}")]
	[TestCase("/test-user", "/{userName}")]
	public void Match_MatchingPartsWithParameter_TrueValueParsed(string currentPath, string controllerRoute)
	{
		// Act
		var result = _matcher.Match(currentPath.GetSplitPath(), new Controller1Route(controllerRoute));

		// Assert

		Assert.That(result.Success, Is.True);
		Assert.That(result.RouteParameters["userName"], Is.EqualTo("test-user"));
	}

	[TestCase("/foo/bar", "/{test}/{name}")]
	public void Match_TwoSegmentsWithTwoParameters_TrueParsed(string currentPath, string controllerRoute)
	{
		// Act
		var result = _matcher.Match(currentPath.GetSplitPath(), new Controller1Route(controllerRoute));

		// Assert

		Assert.That(result.Success, Is.True);
		Assert.That(result.RouteParameters["test"], Is.EqualTo("foo"));
		Assert.That(result.RouteParameters["name"], Is.EqualTo("bar"));
	}

	[TestCase("/15", "/{id:int}")]
	public void Match_OneSegmentWithOneIntegerParameter_True(string currentPath, string controllerRoute)
	{
		// Act
		var result = _matcher.Match(currentPath.GetSplitPath(), new Controller1Route(controllerRoute));

		// Assert

		Assert.That(result.Success, Is.True);
		Assert.That(result.RouteParameters["id"], Is.EqualTo(15));
	}

	[TestCase("/15", "/{id:decimal}")]
	public void Match_OneSegmentWithOneDecimalParameter_True(string currentPath, string controllerRoute)
	{
		// Act
		var result = _matcher.Match(currentPath.GetSplitPath(), new Controller1Route(controllerRoute));

		// Assert

		Assert.That(result.Success, Is.True);
		Assert.That(result.RouteParameters["id"], Is.EqualTo((decimal)15));
	}

	[TestCase("/true", "/{foo:bool}")]
	public void Match_BoolParameter_True(string currentPath, string controllerRoute)
	{
		// Act
		var result = _matcher.Match(currentPath.GetSplitPath(), new Controller1Route(controllerRoute));

		// Assert

		Assert.That(result.Success, Is.True);
		Assert.That(result.RouteParameters["foo"], Is.EqualTo(true));
	}

	[TestCase("/hello,world,test", "/{foo:[]}")]
	public void Match_StringArrayShortVersionParameter_True(string currentPath, string controllerRoute)
	{
		// Act
		var result = _matcher.Match(currentPath.GetSplitPath(), new Controller1Route(controllerRoute));

		// Assert

		Assert.That(result.Success, Is.True);

		var items = (IList<string>)result.RouteParameters["foo"];

		Assert.That(items.Count, Is.EqualTo(3));
		Assert.That(items[0], Is.EqualTo("hello"));
		Assert.That(items[1], Is.EqualTo("world"));
		Assert.That(items[2], Is.EqualTo("test"));
	}

	[TestCase("/1,2,3", "/{foo:int[]}")]
	public void Match_IntArrayShortVersionParameter_True(string currentPath, string controllerRoute)
	{
		// Act
		var result = _matcher.Match(currentPath.GetSplitPath(), new Controller1Route(controllerRoute));

		// Assert

		Assert.That(result.Success, Is.True);

		var items = (IList<int>)result.RouteParameters["foo"];

		Assert.That(items.Count, Is.EqualTo(3));
		Assert.That(items[0], Is.EqualTo(1));
		Assert.That(items[1], Is.EqualTo(2));
		Assert.That(items[2], Is.EqualTo(3));
	}

	[TestCase("/1,2,3", "/{foo:decimal[]}")]
	public void Match_DecimalArrayShortVersionParameter_True(string currentPath, string controllerRoute)
	{
		// Arrange
		var result = _matcher.Match(currentPath.GetSplitPath(), new Controller1Route(controllerRoute));

		// Assert

		Assert.That(result.Success, Is.True);

		var items = (IList<decimal>)result.RouteParameters["foo"];

		Assert.That(items.Count, Is.EqualTo(3));
		Assert.That(items[0], Is.EqualTo(1));
		Assert.That(items[1], Is.EqualTo(2));
		Assert.That(items[2], Is.EqualTo(3));
	}

	[TestCase("/true,false,str", "/{foo:bool[]}")]
	public void Match_BoolArrayShortVersionParameterWithTypeMismatch_True(string currentPath, string controllerRoute)
	{
		// Arrange
		var result = _matcher.Match(currentPath.GetSplitPath(), new Controller1Route(controllerRoute));

		// Assert

		Assert.That(result.Success, Is.True);

		var items = (IList<bool>)result.RouteParameters["foo"];

		Assert.That(items.Count, Is.EqualTo(2));
		Assert.That(items[0], Is.EqualTo(true));
		Assert.That(items[1], Is.EqualTo(false));
	}
}