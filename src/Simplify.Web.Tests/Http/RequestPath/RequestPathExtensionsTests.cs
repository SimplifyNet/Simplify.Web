using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Http.RequestPath;

namespace Simplify.Web.Tests.Http.RequestPath;

[TestFixture]
public class RequestPathExtensionsTests
{
	[Test]
	public void GetRelativeFilePath_PathWhtStartSlash_StartSlashTrimmed()
	{
		// Assign

		var request = new Mock<HttpRequest>();
		request.SetupGet(x => x.Path).Returns(new PathString("/test"));

		// Act
		var result = request.Object.GetRelativeFilePath();

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
		var result = request.Object.GetRelativeFilePath();

		// Assert

		Assert.AreEqual("", result);
	}
}