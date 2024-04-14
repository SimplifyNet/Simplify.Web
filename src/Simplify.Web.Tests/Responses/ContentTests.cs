using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Core;
using Simplify.Web.Modules;
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
		// Assign

		var content = new Mock<Content>("test", 123, "") { CallBase = true };
		content.SetupGet(x => x.ResponseWriter).Returns(_responseWriter.Object);
		content.SetupGet(x => x.Context).Returns(_context.Object);
		_context.SetupSet(x => x.Response.StatusCode = It.IsAny<int>());

		// Act
		var result = await content.Object.ExecuteAsync();

		// Assert

		_responseWriter.Verify(x => x.WriteAsync(It.Is<string>(d => d == "test"), It.IsAny<HttpResponse>()));
		_context.VerifySet(x => x.Response.StatusCode = It.Is<int>(code => code == 123));
		Assert.AreEqual(ControllerResponseResult.RawOutput, result);
	}
}