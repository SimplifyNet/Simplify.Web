using System;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.StaticFiles.Context;
using Simplify.Web.StaticFiles.IO;

namespace Simplify.Web.Tests.StaticFiles.Context;

[TestFixture]
public class StaticFileProcessingContextFactoryTests
{
	private StaticFileProcessingContextFactory _factory = null!;

	private Mock<IStaticFile> _file = null!;

	[SetUp]
	public void Initialize()
	{
		_file = new Mock<IStaticFile>();
		_factory = new StaticFileProcessingContextFactory(_file.Object);
	}

	[Test]
	public void Create_SomeValuesAndCantBeCached_CopiedToContext()
	{
		// Arrange

		var filePath = "foo";
		var lastModificationTime = new DateTime(2023, 5, 2, 15, 14, 0);
		var httpContext = Mock.Of<HttpContext>(x => x.Request == Mock.Of<HttpRequest>(r => r.Headers == new HeaderDictionary()));

		_file.Setup(x => x.GetLastModificationTime(It.Is<string>(x => x == filePath))).Returns(lastModificationTime);

		// Act
		var context = _factory.Create(httpContext, filePath);

		// Assert

		Assert.That(context.RelativeFilePath, Is.EqualTo(filePath));
		Assert.That(context.LastModificationTime, Is.EqualTo(lastModificationTime));
		Assert.That(context.CanBeCached, Is.False);
	}
}
