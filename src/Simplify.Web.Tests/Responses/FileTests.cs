using Microsoft.Owin;
using Moq;
using NUnit.Framework;
using Simplify.Web.Modules;
using Simplify.Web.Responses;

namespace Simplify.Web.Tests.Responses;

[TestFixture]
public class FileTests
{
	private Mock<IWebContext> _context;
	private Mock<IHeaderDictionary> _headerDictionary;

	[SetUp]
	public void Initialize()
	{
		_context = new Mock<IWebContext>();
		_headerDictionary = new Mock<IHeaderDictionary>();

		_context.SetupGet(x => x.Response.Headers).Returns(_headerDictionary.Object);
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

		_headerDictionary.Verify(x => x.Append(It.Is<string>(d => d == "Content-Disposition"), It.Is<string>(d => d == "attachment; filename=\"Foo.txt\"")));
		_context.VerifySet(x => x.Response.ContentType = "application/example");
		_context.Verify(x => x.Response.Write(It.Is<byte[]>(d => d[0] == 13)));
		Assert.AreEqual(ControllerResponseResult.RawOutput, result);
	}
}