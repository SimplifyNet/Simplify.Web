using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers;
using Simplify.Web.Controllers.Execution;
using Simplify.Web.Modules.Redirection;

namespace Simplify.Web.Tests.Controllers.Execution;

[TestFixture]
public class PreviousPageUrlUpdaterTests
{
	private readonly IReadOnlyList<IMatchedController> _controllers = new List<IMatchedController>();
	private readonly HttpContext _context = Mock.Of<HttpContext>();

	private PreviousPageUrlUpdater _updater = null!;

	private Mock<IControllersExecutor> _baseExecutor = null!;
	private Mock<IRedirector> _redirector = null!;

	[SetUp]
	public void Initialize()
	{
		_baseExecutor = new Mock<IControllersExecutor>();
		_redirector = new Mock<IRedirector>();
		_updater = new PreviousPageUrlUpdater(_baseExecutor.Object, _redirector.Object);
	}

	[Test]
	public async Task ExecuteAsync_DefaultBehavior_UrlSet()
	{
		// Act
		var result = await _updater.ExecuteAsync(null!, null!);

		// Assert

		Assert.That(result, Is.EqualTo(ResponseBehavior.Default));

		_redirector.Verify(x => x.SetPreviousPageUrlToCurrentPage());
	}

	[TestCase(ResponseBehavior.RawOutput)]
	[TestCase(ResponseBehavior.Redirect)]
	public async Task Execute_NonDefaultBehavior_UrlIsNotSet(ResponseBehavior behavior)
	{
		// Arrange
		_baseExecutor.Setup(x => x.ExecuteAsync(It.Is<IReadOnlyList<IMatchedController>>(c => c == _controllers), It.Is<HttpContext>(c => c == _context)))
			.ReturnsAsync(behavior);

		// Act
		var result = await _updater.ExecuteAsync(_controllers, _context);

		// Assert
		Assert.That(result, Is.EqualTo(behavior));

		_redirector.Verify(x => x.SetPreviousPageUrlToCurrentPage(), Times.Never);
	}
}