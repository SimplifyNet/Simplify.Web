using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.System;
using Simplify.Web.Http.ResponseWriting;
using Simplify.Web.StaticFiles.Context;
using Simplify.Web.StaticFiles.Handlers;
using Simplify.Web.StaticFiles.IO;
using TimeProvider = Simplify.System.TimeProvider;

namespace Simplify.Web.Tests.StaticFiles.Handlers;

[TestFixture]
public class NewFileHandlerTests
{
	private NewFileHandler _handler = null!;

	private Mock<IResponseWriter> _responseWriter = null!;
	private Mock<IStaticFile> _staticFile = null!;

	[SetUp]
	public void Initialize()
	{
		_responseWriter = new Mock<IResponseWriter>();
		_staticFile = new Mock<IStaticFile>();
		_handler = new NewFileHandler(_responseWriter.Object, _staticFile.Object);
	}

	[Test]
	public void CanHandle_CanBeCached_False()
	{
		// Arrange
		var context = Mock.Of<IStaticFileProcessingContext>(x => x.CanBeCached);

		// Act
		var result = _handler.CanHandle(context);

		// Assert
		Assert.That(result, Is.False);
	}

	[Test]
	public void CanHandle_CantBeCached_True()
	{
		// Arrange

		var context = Mock.Of<IStaticFileProcessingContext>(x => !x.CanBeCached);

		// Act
		var result = _handler.CanHandle(context);

		// Assert
		Assert.That(result, Is.True);
	}

	[Test]
	public async Task ExecuteAsync_NewFile_FileSendToClientAndRespectiveResponsePropertiesAreSet()
	{
		// Arrange

		var filePath = "Foo.txt";
		var lastModificationTime = new DateTime(2013, 4, 5, 0, 0, 0, DateTimeKind.Utc);
		var data = new byte[1] { 255 };

		var context = Mock.Of<IStaticFileProcessingContext>(x =>
			x.RelativeFilePath == filePath &&
			x.LastModificationTime == lastModificationTime);

		TimeProvider.Current = Mock.Of<ITimeProvider>(x => x.Now == new DateTime(2013, 1, 1, 0, 0, 0, DateTimeKind.Utc));

		_staticFile.Setup(x => x.GetDataAsync(It.Is<string>(s => s == filePath))).Returns(Task.FromResult(data));

		var response = Mock.Of<HttpResponse>(x => x.Headers == new HeaderDictionary());

		// Act
		await _handler.ExecuteAsync(context, response);

		// Assert

		Assert.That(response.ContentType, Is.EqualTo("text/plain"));
		Assert.That(response.Headers["Last-Modified"], Is.EqualTo(lastModificationTime.ToString("r")));
		Assert.That(response.Headers["Expires"], Is.EqualTo(new DateTimeOffset(new DateTime(2014, 1, 1, 0, 0, 0, DateTimeKind.Utc)).ToString("R")));

		_responseWriter.Verify(x => x.WriteAsync(It.Is<HttpResponse>(r => r == response), It.Is<byte[]>(b => b == data)));
	}
}