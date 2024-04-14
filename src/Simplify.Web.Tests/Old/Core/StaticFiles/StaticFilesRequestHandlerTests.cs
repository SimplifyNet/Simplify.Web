using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Core.StaticFiles;

namespace Simplify.Web.Tests.Old.Core.StaticFiles;

[TestFixture]
public class StaticFilesRequestHandlerTests
{
	private StaticFilesRequestHandler _requestHandler = null!;
	private Mock<IStaticFileHandler> _fileHandler = null!;
	private Mock<IStaticFileResponseFactory> _responseFactory = null!;
	private Mock<IStaticFileResponse> _response = null!;

	private Mock<HttpContext> _context = null!;

	[SetUp]
	public void Initialize()
	{
		_fileHandler = new Mock<IStaticFileHandler>();

		_response = new Mock<IStaticFileResponse>();

		_responseFactory = new Mock<IStaticFileResponseFactory>();
		_responseFactory.Setup(x => x.Create(It.IsAny<HttpResponse>())).Returns(_response.Object);

		_requestHandler = new StaticFilesRequestHandler(_fileHandler.Object, _responseFactory.Object);

		_context = new Mock<HttpContext>();
		_context.SetupGet(x => x.Request.Headers).Returns(new HeaderDictionary(0));
	}

	[Test]
	public async Task ProcessRequest_CacheEnabled_SendNotModifiedCalled()
	{
		// Assign
		_fileHandler.Setup(x => x.IsFileCanBeUsedFromCache(It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime>()))
			.Returns(true);

		// Act
		await _requestHandler.ProcessRequest(_context.Object);

		// Assert
		_response.Verify(x => x.SendNotModified(It.IsAny<DateTime>(), It.IsAny<string>()));
	}

	[Test]
	public async Task ProcessRequest_CacheDisabled_SendNotModifiedCalled()
	{
		// Assign
		_fileHandler.Setup(x => x.IsFileCanBeUsedFromCache(It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime>()))
			.Returns(false);

		// Act
		await _requestHandler.ProcessRequest(_context.Object);

		// Assert
		_response.Verify(x => x.SendNew(It.IsAny<byte[]>(), It.IsAny<DateTime>(), It.IsAny<string>()));
	}
}