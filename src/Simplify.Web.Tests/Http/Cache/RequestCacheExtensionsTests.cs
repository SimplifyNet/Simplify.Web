using System;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Http.Cache;

namespace Simplify.Web.Tests.Http.Cache;

[TestFixture]
public class RequestCacheExtensionsTests
{
	[Test]
	public void IsFileCanBeUsedFromCache_FileLastModifiedTimeLessThanIfModifiedSinceTime_True()
	{
		// Arrange

		var lastModificationTime = new DateTime(2015, 10, 21, 07, 27, 0);

		var request = Mock.Of<HttpRequest>(r => r.Headers == new HeaderDictionary
		{
			{ "If-Modified-Since", "Wed, 21 Oct 2015 07:28:00 GMT" }
		});

		// Act
		var result = request.IsFileCanBeUsedFromCache(lastModificationTime);

		// Assert
		Assert.That(result, Is.True);
	}

	[Test]
	public void IsFileCanBeUsedFromCache_FileLastModifiedTimeEqualsIfModifiedSinceTime_True()
	{
		// Arrange

		var lastModificationTime = new DateTime(2015, 10, 21, 07, 28, 0);

		var request = Mock.Of<HttpRequest>(r => r.Headers == new HeaderDictionary
		{
			{ "If-Modified-Since", "Wed, 21 Oct 2015 07:28:00 GMT" }
		});

		// Act
		var result = request.IsFileCanBeUsedFromCache(lastModificationTime);

		// Assert
		Assert.That(result, Is.True);
	}

	[Test]
	public void IsFileCanBeUsedFromCache_FileLastModifiedTimeGreaterThanIfModifiedSinceTime_False()
	{
		// Arrange

		var lastModificationTime = new DateTime(2015, 10, 21, 07, 28, 1);

		var request = Mock.Of<HttpRequest>(r => r.Headers == new HeaderDictionary
		{
			{ "If-Modified-Since", "Wed, 21 Oct 2015 07:28:00 GMT" }
		});

		// Act
		var result = request.IsFileCanBeUsedFromCache(lastModificationTime);

		// Assert
		Assert.That(result, Is.False);
	}

	[Test]
	public void IsFileCanBeUsedFromCache_IfModifiedSinceTimeIsNotPresent_False()
	{
		// Arrange

		var lastModificationTime = new DateTime(2015, 10, 21, 07, 28, 1);

		var request = Mock.Of<HttpRequest>(r => r.Headers == new HeaderDictionary
		{
		});

		// Act
		var result = request.IsFileCanBeUsedFromCache(lastModificationTime);

		// Assert
		Assert.That(result, Is.False);
	}

	[Test]
	public void IsFileCanBeUsedFromCache_NoCacheRequestedHeaderPresentButValidToCacheByTime_False()
	{
		// Arrange

		var lastModificationTime = new DateTime(2015, 10, 21, 07, 28, 0);

		var request = Mock.Of<HttpRequest>(r => r.Headers == new HeaderDictionary
		{
			{ "If-Modified-Since", "Wed, 21 Oct 2015 07:28:00 GMT" },
			{ "Cache-Control", "no-cache" }
		});

		// Act
		var result = request.IsFileCanBeUsedFromCache(lastModificationTime);

		// Assert
		Assert.That(result, Is.False);
	}
}
