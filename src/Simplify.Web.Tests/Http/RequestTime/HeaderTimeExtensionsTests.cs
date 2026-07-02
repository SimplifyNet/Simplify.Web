using System;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Http.RequestTime;

namespace Simplify.Web.Tests.Http.RequestTime;

[TestFixture]
public class HeaderTimeExtensionsTests
{
	[Test]
	public void GetIfModifiedSinceTime_Exists_Parsed()
	{
		// Arrange

		var time = new DateTime(2016, 03, 04, 0, 0, 0, DateTimeKind.Utc);
		var headers = new Mock<IHeaderDictionary>();
		headers.SetupGet(x => x[It.Is<string>(p => p == "If-Modified-Since")]).Returns(time.ToString("r"));
		headers.Setup(x => x.ContainsKey(It.Is<string>(p => p == "If-Modified-Since"))).Returns(true);

		// Act
		var result = headers.Object.GetIfModifiedSinceTime();

		// Assert

		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.EqualTo(time));
	}

	[Test]
	public void GetIfModifiedSinceTime_NoExists_Null()
	{
		// Arrange

		var headers = new Mock<IHeaderDictionary>();
		headers.Setup(x => x.ContainsKey(It.Is<string>(p => p == "If-Modified-Since"))).Returns(false);

		// Act
		var result = headers.Object.GetIfModifiedSinceTime();

		// Assert
		Assert.That(result, Is.Null);
	}

	[Test]
	public void GetIfModifiedSinceTime_MalformedValue_NullInsteadOfException()
	{
		// Arrange

		// Not exact RFC1123 format (e.g. a non-GMT-suffixed or obsolete date some
		// proxies/clients send) - this used to throw an unhandled FormatException.
		var headers = new Mock<IHeaderDictionary>();
		headers.SetupGet(x => x[It.Is<string>(p => p == "If-Modified-Since")]).Returns("not-a-valid-date");
		headers.Setup(x => x.ContainsKey(It.Is<string>(p => p == "If-Modified-Since"))).Returns(true);

		// Act & Assert
		DateTime? result = null;
		Assert.DoesNotThrow(() => result = headers.Object.GetIfModifiedSinceTime());
		Assert.That(result, Is.Null);
	}
}