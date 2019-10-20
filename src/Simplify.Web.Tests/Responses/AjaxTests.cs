#nullable disable

using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Core;
using Simplify.Web.Modules;
using Simplify.Web.Responses;

namespace Simplify.Web.Tests.Responses
{
	[TestFixture]
	public class AjaxTests
	{
		private Mock<IResponseWriter> _responseWriter;
		private Mock<IWebContext> _context;

		[SetUp]
		public void Initialize()
		{
			_responseWriter = new Mock<IResponseWriter>();
			_context = new Mock<IWebContext>();
		}

		[Test]
		public void Process_NormalData_DataWrittenToResponse()
		{
			// Assign
			var ajax = new Mock<Ajax>("test", 123, "") { CallBase = true };
			ajax.SetupGet(x => x.ResponseWriter).Returns(_responseWriter.Object);
			ajax.SetupGet(x => x.Context).Returns(_context.Object);
			_context.SetupSet(x => x.Response.StatusCode = It.IsAny<int>());

			// Act
			var result = ajax.Object.Process();

			// Assert

			_responseWriter.Verify(x => x.Write(It.Is<string>(d => d == "test"), It.IsAny<HttpResponse>()));
			_context.VerifySet(x => x.Response.StatusCode = It.Is<int>(code => code == 123));
			Assert.AreEqual(ControllerResponseResult.RawOutput, result);
		}
	}
}