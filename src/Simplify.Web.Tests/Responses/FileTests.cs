using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Http.ResponseWriting;
using Simplify.Web.Modules.Context;
using Simplify.Web.Responses;
using File = Simplify.Web.Responses.File;

namespace Simplify.Web.Tests.Responses;

[TestFixture]
public class FileTests
{
	private Mock<IWebContext> _context = null!;
	private Mock<IResponseWriter> _responseWriter = null!;
	private HeaderDictionary _headerDictionary = null!;

	[SetUp]
	public void Initialize()
	{
		_context = new Mock<IWebContext>();
		_responseWriter = new Mock<IResponseWriter>();
		_headerDictionary = [];

		_context.SetupGet(x => x.Response.Headers).Returns(_headerDictionary);
		_context.Setup(x => x.Response.Body.Write(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()));
	}

	[Test]
	public async Task Process_NormalData_FileSent()
	{
		// Arrange

		var data = "\r"u8.ToArray();
		var file = new Mock<File>("Foo.txt", "application/example", data, 200) { CallBase = true };

		file.SetupGet(x => x.Context).Returns(_context.Object);
		file.SetupGet(x => x.ResponseWriter).Returns(_responseWriter.Object);

		// Act
		var result = await file.Object.ExecuteAsync();

		// Assert

		Assert.That(_headerDictionary.Count, Is.EqualTo(1));
		Assert.That(_headerDictionary["Content-Disposition"], Is.EqualTo("attachment; filename=\"Foo.txt\""));
		Assert.That(result, Is.EqualTo(ResponseBehavior.RawOutput));

		_context.VerifySet(x => x.Response.ContentType = "application/example");
		_responseWriter.Verify(x => x.WriteAsync(It.IsAny<HttpResponse>(), It.Is<byte[]>(d => d == data)));
	}

	[Test]
	public async Task Process_InlineBytesWithCachingHeaders_HeadersSetAndBytesSent()
	{
		// Arrange

		var data = "\r"u8.ToArray();
		var file = new Mock<File>(data, "application/example", "Foo.txt",
			ContentDispositionType.Inline, "public, max-age=31536000, immutable", "\"ABC123\"", 200)
		{ CallBase = true };

		file.SetupGet(x => x.Context).Returns(_context.Object);
		file.SetupGet(x => x.ResponseWriter).Returns(_responseWriter.Object);

		// Act
		var result = await file.Object.ExecuteAsync();

		// Assert

		Assert.That(result, Is.EqualTo(ResponseBehavior.RawOutput));
		Assert.That(_headerDictionary["Content-Disposition"], Is.EqualTo("inline; filename=\"Foo.txt\""));
		Assert.That(_headerDictionary["Cache-Control"], Is.EqualTo("public, max-age=31536000, immutable"));
		Assert.That(_headerDictionary["ETag"], Is.EqualTo("\"ABC123\""));

		_context.VerifySet(x => x.Response.ContentType = "application/example");
		_responseWriter.Verify(x => x.WriteAsync(It.IsAny<HttpResponse>(), It.Is<byte[]>(d => d == data)));
	}

	[Test]
	public async Task Process_Stream_StreamSentAndDisposed()
	{
		// Arrange

		var stream = new DisposableMemoryStream();

		var file = new Mock<File>(stream, "application/example", null!,
			ContentDispositionType.Inline, null!, null!, 200)
		{ CallBase = true };

		file.SetupGet(x => x.Context).Returns(_context.Object);
		file.SetupGet(x => x.ResponseWriter).Returns(_responseWriter.Object);

		// Act
		var result = await file.Object.ExecuteAsync();

		// Assert

		Assert.That(result, Is.EqualTo(ResponseBehavior.RawOutput));
		Assert.That(_headerDictionary["Content-Disposition"], Is.EqualTo("inline"));

		_responseWriter.Verify(x => x.WriteAsync(It.IsAny<HttpResponse>(), It.Is<Stream>(s => s == stream)));
		Assert.That(stream.Disposed, Is.True);
	}

	private class DisposableMemoryStream : MemoryStream
	{
		public bool Disposed { get; private set; }

		protected override void Dispose(bool disposing)
		{
			Disposed = true;
			base.Dispose(disposing);
		}

#if !NETSTANDARD2_0
		public override async ValueTask DisposeAsync()
		{
			Disposed = true;
			await base.DisposeAsync();
		}
#endif
	}
}
