using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.StaticFiles.Context;
using Simplify.Web.StaticFiles.Handlers;

namespace Simplify.Web.Tests.StaticFiles.Handlers;

[TestFixture]
public class ClientCachedFileHandlerTests
{
	private ClientCachedFileHandler _handler = null!;

	[SetUp]
	public void Initialize() => _handler = new ClientCachedFileHandler();

	[Test]
	public void CanHandle_CanBeCached_True()
	{
		// Arrange
		var context = Mock.Of<IStaticFileProcessingContext>(x => x.CanBeCached);

		// Act
		var result = _handler.CanHandle(context);

		// Assert
		Assert.That(result, Is.True);
	}

	[Test]
	public void CanHandle_CantBeCached_False()
	{
		// Arrange

		var context = Mock.Of<IStaticFileProcessingContext>(x => !x.CanBeCached);

		// Act
		var result = _handler.CanHandle(context);

		// Assert
		Assert.That(result, Is.False);
	}

	[Test]
	public async Task ExecuteAsync_CachedFile_RespectiveResponsePropertiesAreSet()
	{
		// Arrange

		const string filePath = "Foo.txt";
		var lastModificationTime = new DateTime(2013, 4, 5, 0, 0, 0, DateTimeKind.Utc);

		var context = Mock.Of<IStaticFileProcessingContext>(x =>
			x.RelativeFilePath == filePath &&
			x.LastModificationTime == lastModificationTime);

		var response = Mock.Of<HttpResponse>(x => x.Headers == new HeaderDictionary());

		// Act
		await _handler.ExecuteAsync(context, response);

		// Assert

		Assert.That(response.StatusCode, Is.EqualTo((int)HttpStatusCode.NotModified));
		Assert.That(response.ContentType, Is.EqualTo("text/plain"));
		Assert.That(response.Headers["Last-Modified"], Is.EqualTo(lastModificationTime.ToString("r")));
	}
}