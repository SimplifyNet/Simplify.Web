#nullable disable

using System;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Core.StaticFiles;

namespace Simplify.Web.Tests.Core.StaticFiles
{
	[TestFixture]
	public class StaticFilesRequestHandlerTests
	{
		private StaticFilesRequestHandler _requestHandler;
		private Mock<IStaticFileHandler> _fileHandler;
		private Mock<IStaticFileResponseFactory> _responseFactory;
		private Mock<IStaticFileResponse> _response;

		private Mock<HttpContext> _context;

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
		public void ProcessRequest_CacheEnabled_SendNotModifiedCalled()
		{
			// Assign
			_fileHandler.Setup(x => x.IsFileCanBeUsedFromCache(It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime>()))
				.Returns(true);

			// Act
			_requestHandler.ProcessRequest(_context.Object);

			// Assert
			_response.Verify(x => x.SendNotModified(It.IsAny<DateTime>(), It.IsAny<string>()));
		}

		[Test]
		public void ProcessRequest_CacheDisabled_SendNotModifiedCalled()
		{
			// Assign
			_fileHandler.Setup(x => x.IsFileCanBeUsedFromCache(It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime>()))
				.Returns(false);

			// Act
			_requestHandler.ProcessRequest(_context.Object);

			// Assert
			_response.Verify(x => x.SendNew(It.IsAny<byte[]>(), It.IsAny<DateTime>(), It.IsAny<string>()));
		}
	}
}