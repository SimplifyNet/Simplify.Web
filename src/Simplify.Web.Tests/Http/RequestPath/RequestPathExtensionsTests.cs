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
		// Arrange

		var request = new Mock<HttpRequest>();
		request.SetupGet(x => x.Path).Returns(new PathString("/test"));

		// Act
		var result = request.Object.GetRelativeFilePath();

		// Assert

		Assert.That(result, Is.EqualTo("test"));
	}

	[Test]
	public void GetRelativeFilePath_EmptyPath_EmptyString()
	{
		// Arrange

		var request = new Mock<HttpRequest>();
		request.SetupGet(x => x.Path).Returns(new PathString());

		// Act
		var result = request.Object.GetRelativeFilePath();

		// Assert

		Assert.That(result, Is.EqualTo(""));
	}
}