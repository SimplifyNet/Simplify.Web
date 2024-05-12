using NUnit.Framework;
using Simplify.Web.Http.Cache;

namespace Simplify.Web.Tests.StaticFiles.Context;

[TestFixture]
public class CacheControlHeaderExtensions
{
	[Test]
	public void IsNoCacheRequested_NullHeader_False()
	{
		// Arrange
		string str = null!;

		// Act
		var result = str.IsNoCacheRequested();

		// Assert
		Assert.That(result, Is.False);
	}

	[Test]
	public void IsNoCacheRequested_ContainsNoCache_True()
	{
		// Act
		var result = "no-cache".IsNoCacheRequested();

		// Assert
		Assert.That(result, Is.True);
	}
}