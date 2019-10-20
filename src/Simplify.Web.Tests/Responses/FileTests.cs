#nullable disable

using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Modules;
using Simplify.Web.Responses;

namespace Simplify.Web.Tests.Responses
{
	[TestFixture]
	public class FileTests
	{
		private Mock<IWebContext> _context;
		private HeaderDictionary _headerDictionary;

		[SetUp]
		public void Initialize()
		{
			_context = new Mock<IWebContext>();
			_headerDictionary = new HeaderDictionary();

			_context.SetupGet(x => x.Response.Headers).Returns(_headerDictionary);
			_context.Setup(x => x.Response.Body.Write(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()));
		}

		[Test]
		public void Process_NormalData_FileSent()
		{
			// Assign

			var file = new Mock<File>("Foo.txt", "application/example", new byte[] { 13 }, 200) { CallBase = true };
			file.SetupGet(x => x.Context).Returns(_context.Object);

			// Act
			var result = file.Object.Process();

			// Assert

			_context.VerifySet(x => x.Response.ContentType = "application/example");
			_context.Verify(x => x.Response.Body.Write(It.Is<byte[]>(d => d[0] == 13), It.Is<int>(data => data == 0), It.Is<int>(data => data == 1)));
			Assert.AreEqual(1, _headerDictionary.Count);
			Assert.AreEqual("attachment; filename=\"Foo.txt\"", _headerDictionary["Content-Disposition"]);
			Assert.AreEqual(ControllerResponseResult.RawOutput, result);
		}
	}
}