using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Http.ResponseWriting;
using Simplify.Web.Modules.Context;
using Simplify.Web.Responses;

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
}