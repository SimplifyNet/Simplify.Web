using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Http.ResponseWriting;
using Simplify.Web.Modules.Context;
using Simplify.Web.Responses;

namespace Simplify.Web.Tests.Responses;

[TestFixture]
public class ContentTests
{
	private Mock<IResponseWriter> _responseWriter = null!;
	private Mock<IWebContext> _context = null!;

	[SetUp]
	public void Initialize()
	{
		_responseWriter = new Mock<IResponseWriter>();
		_context = new Mock<IWebContext>();
	}

	[Test]
	public async Task Process_NormalData_DataWrittenToResponse()
	{
		// Arrange

		var content = new Mock<Content>("test", 123, "") { CallBase = true };

		content.SetupGet(x => x.ResponseWriter).Returns(_responseWriter.Object);
		content.SetupGet(x => x.Context).Returns(_context.Object);
		_context.SetupSet(x => x.Response.StatusCode = It.IsAny<int>());

		// Act
		var result = await content.Object.ExecuteAsync();

		// Assert

		Assert.That(result, Is.EqualTo(ResponseBehavior.RawOutput));

		_responseWriter.Verify(x => x.WriteAsync(It.IsAny<HttpResponse>(), It.Is<string>(d => d == "test")));
		_context.VerifySet(x => x.Response.StatusCode = It.Is<int>(code => code == 123));
	}
}