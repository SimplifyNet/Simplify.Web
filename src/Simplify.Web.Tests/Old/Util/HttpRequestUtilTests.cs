using System;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Old.Util;

namespace Simplify.Web.Tests.Old.Util;

[TestFixture]
public class HttpRequestUtilTests
{
	[Test]
	public void GetIfModifiedSinceTime_Exists_Parsed()
	{
		// Assign

		var time = new DateTime(2016, 03, 04);
		var headers = new Mock<IHeaderDictionary>();
		headers.SetupGet(x => x[It.Is<string>(p => p == "If-Modified-Since")]).Returns(time.ToString("r"));
		headers.Setup(x => x.ContainsKey(It.Is<string>(p => p == "If-Modified-Since"))).Returns(true);

		// Act
		var result = HttpRequestUtil.GetIfModifiedSinceTime(headers.Object);

		// Assert

		Assert.IsNotNull(result);
		Assert.AreEqual(time, result);
	}

	[Test]
	public void GetIfModifiedSinceTime_NoExists_Null()
	{
		// Assign

		var headers = new Mock<IHeaderDictionary>();
		headers.Setup(x => x.ContainsKey(It.Is<string>(p => p == "If-Modified-Since"))).Returns(false);

		// Act
		var result = HttpRequestUtil.GetIfModifiedSinceTime(headers.Object);

		// Assert

		Assert.IsNull(result);
	}

	[Test]
	public void IsNoCacheRequested_NullHeader_False()
	{
		// Act
		var result = HttpRequestUtil.IsNoCacheRequested(null!);

		// Assert

		Assert.IsFalse(result);
	}

	[Test]
	public void IsNoCacheRequested_ContainsNoCache_True()
	{
		// Act
		var result = HttpRequestUtil.IsNoCacheRequested("no-cache");

		// Assert

		Assert.IsTrue(result);
	}

	[Test]
	public void GetRelativeFilePath_PathWhtStartSlash_StartSlashTrimmed()
	{
		// Assign

		var request = new Mock<HttpRequest>();
		request.SetupGet(x => x.Path).Returns(new PathString("/test"));

		// Act
		var result = HttpRequestUtil.GetRelativeFilePath(request.Object);

		// Assert

		Assert.AreEqual("test", result);
	}

	[Test]
	public void GetRelativeFilePath_EmptyPath_EmptyString()
	{
		// Assign

		var request = new Mock<HttpRequest>();
		request.SetupGet(x => x.Path).Returns(new PathString());

		// Act
		var result = HttpRequestUtil.GetRelativeFilePath(request.Object);

		// Assert

		Assert.AreEqual("", result);
	}
}