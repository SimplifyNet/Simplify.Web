using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Http;
using Simplify.Web.Old.Core.Controllers;
using Simplify.Web.Old.Core.Controllers.Execution;
using Simplify.Web.Old.Meta;
using Simplify.Web.Old.Modules;
using Simplify.Web.Old.Routing;
using Simplify.Web.Tests.Old.Core.Controllers.TestTypes;

namespace Simplify.Web.Tests.Old.Core.Controllers;

[TestFixture]
public class ControllersProcessorTests
{
	private readonly IDictionary<string, object> _routeParameters = new Dictionary<string, object>();
	private ControllersProcessor _processor = null!;
	private Mock<IControllersAgent> _agent = null!;
	private Mock<IControllerExecutor> _controllersExecutor = null!;
	private Mock<IRedirector> _redirector = null!;
	private Mock<HttpContext> _context = null!;

	private IControllerMetaData _metaData = null!;

	[SetUp]
	public void Initialize()
	{
		_agent = new Mock<IControllersAgent>();
		_controllersExecutor = new Mock<IControllerExecutor>();
		_redirector = new Mock<IRedirector>();
		_processor = new ControllersProcessor(_agent.Object, _controllersExecutor.Object, _redirector.Object);

		_context = new Mock<HttpContext>();

		_metaData = Mock.Of<IControllerMetaData>(x => x.ControllerType == typeof(TestController1) &&
			x.ExecParameters == new ControllerExecParameters(new Dictionary<HttpMethod, string> { { HttpMethod.Put, "/foo/bar" } }, 0));

		_agent.Setup(x => x.MatchControllerRoute(It.IsAny<IControllerMetaData>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new RouteMatchResult(true, _routeParameters));
		_agent.Setup(x => x.GetStandardControllersMetaData()).Returns(() => new List<IControllerMetaData>
		{
			_metaData
		});

		_agent.Setup(x => x.IsSecurityRulesViolated(It.IsAny<IControllerMetaData>(), It.IsAny<ClaimsPrincipal>())).Returns(SecurityRuleCheckResult.Ok);

		// Setup current URL

		_context.SetupGet(x => x.Request.Scheme).Returns("http");
		_context.SetupGet(x => x.Request.Host).Returns(new HostString("localhost", 8080));
		_context.SetupGet(x => x.Request.Path).Returns(new PathString("/foo/bar"));
		_context.SetupGet(x => x.Request.Method).Returns("GET");
	}

	[Test]
	public async Task ProcessControllers_NoControllersMatchedNo404Controller_404Returned()
	{
		// Arrange
		_agent.Setup(x => x.MatchControllerRoute(It.IsAny<IControllerMetaData>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new RouteMatchResult());

		// Act
		var result = await _processor.ProcessControllers(null!, _context.Object);

		// Assert

		Assert.That(result, Is.EqualTo(ControllersProcessorResult.Http404));

		_controllersExecutor.Verify(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController1))),
			Times.Never);
	}

	[Test]
	public async Task ProcessControllers_NoControllersMatchedButHave404Controller_404ControllerExecuted()
	{
		// Arrange

		_agent.Setup(x => x.MatchControllerRoute(It.IsAny<IControllerMetaData>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new RouteMatchResult());
		_agent.Setup(
				x => x.GetHandlerController(It.Is<HandlerControllerType>(d => d == HandlerControllerType.Http404Handler)))
			.Returns(new ControllerMetaData(typeof(TestController2)));

		// Act
		var result = await _processor.ProcessControllers(null!, _context.Object);

		// Assert

		Assert.That(result, Is.EqualTo(ControllersProcessorResult.Ok));

		_controllersExecutor.Verify(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController1))),
			Times.Never);

		_controllersExecutor.Verify(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController2) && t.RouteParameters == null)));

		_redirector.VerifySet(x => x.PreviousPageUrl = It.IsAny<string>(), Times.Never);
	}

	[Test]
	public async Task ProcessControllers_NoControllersMatchedButHave404ControllerRawResult_404ControllerExecutedRawReturned()
	{
		// Arrange

		_agent.Setup(x => x.MatchControllerRoute(It.IsAny<IControllerMetaData>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new RouteMatchResult());
		_agent.Setup(x => x.GetHandlerController(It.Is<HandlerControllerType>(d => d == HandlerControllerType.Http404Handler)))
			.Returns(new ControllerMetaData(typeof(TestController2)));

		_controllersExecutor.Setup(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController2))))
				.Returns(Task.FromResult(Web.Old.ControllerResponseResult.RawOutput));

		// Act
		var result = await _processor.ProcessControllers(null!, _context.Object);

		// Assert

		Assert.That(result, Is.EqualTo(ControllersProcessorResult.RawOutput));

		_controllersExecutor.Verify(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController1))),
			Times.Never);

		_controllersExecutor.Verify(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController2) && t.RouteParameters == null)));
	}

	[Test]
	public async Task ProcessControllers_NoControllersMatchedButHave404ControllerRedirect_404ControllerExecutedRedirectReturned()
	{
		// Arrange

		_agent.Setup(x => x.MatchControllerRoute(It.IsAny<IControllerMetaData>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new RouteMatchResult());
		_agent.Setup(x => x.GetHandlerController(It.Is<HandlerControllerType>(d => d == HandlerControllerType.Http404Handler)))
			.Returns(new ControllerMetaData(typeof(TestController2)));

		_controllersExecutor.Setup(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController2))))
				.Returns(Task.FromResult(Web.Old.ControllerResponseResult.Redirect));

		// Act
		var result = await _processor.ProcessControllers(null!, _context.Object);

		// Assert

		Assert.That(result, Is.EqualTo(ControllersProcessorResult.Redirect));

		_controllersExecutor.Verify(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController1))),
			Times.Never);

		_controllersExecutor.Verify(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController2) && t.RouteParameters == null)));
	}

	[Test]
	public async Task ProcessControllers_OnlyAnyPageControllerMatchedButHave404Controller_404ControllerExecuted()
	{
		// Arrange

		_agent.Setup(x => x.MatchControllerRoute(It.IsAny<IControllerMetaData>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new RouteMatchResult(true));
		_agent.Setup(x => x.GetHandlerController(It.Is<HandlerControllerType>(d => d == HandlerControllerType.Http404Handler)))
			.Returns(new ControllerMetaData(typeof(TestController2)));

		_agent.Setup(x => x.IsAnyPageController(It.IsAny<IControllerMetaData>())).Returns(true);

		// Act
		var result = await _processor.ProcessControllers(null!, _context.Object);

		// Assert

		Assert.That(result, Is.EqualTo(ControllersProcessorResult.Ok));

		_agent.Verify(x => x.IsAnyPageController(It.IsAny<IControllerMetaData>()));

		_controllersExecutor.Verify(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController1))));

		_controllersExecutor.Verify(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController2) && t.RouteParameters == null)));
	}

	[Test]
	public async Task ProcessControllers_StandardControllerMatched_Executed()
	{
		// Arrange
		_agent.Setup(x => x.IsAnyPageController(It.IsAny<IControllerMetaData>())).Returns(false);

		// Act
		var result = await _processor.ProcessControllers(null!, _context.Object);

		// Assert

		Assert.That(result, Is.EqualTo(ControllersProcessorResult.Ok));

		_agent.Verify(x => x.IsAnyPageController(It.IsAny<IControllerMetaData>()));

		_controllersExecutor.Verify(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController1))));

		_controllersExecutor.Verify(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController2) && t.RouteParameters == null)),
			Times.Never);

		_redirector.Verify(x => x.SetPreviousPageUrlToCurrentPage());
	}

	[Test]
	public async Task ProcessControllers_StandardControllersOneIsMatchedNull_ExecutedOnce()
	{
		// Arrange

		_agent.Setup(x => x.IsAnyPageController(It.IsAny<IControllerMetaData>()))
			.Returns(false);

		_agent.SetupSequence(x =>
			 x.MatchControllerRoute(It.IsAny<IControllerMetaData>(), It.IsAny<string>(), It.IsAny<string>()))
				.Returns(new RouteMatchResult(true, _routeParameters))
				.Returns((IRouteMatchResult?)null);

		_agent.Setup(x => x.GetStandardControllersMetaData()).Returns(() => new List<IControllerMetaData>
		{
			_metaData,
			_metaData
		});

		// Act
		var result = await _processor.ProcessControllers(null!, _context.Object);

		// Assert

		Assert.That(result, Is.EqualTo(ControllersProcessorResult.Ok));

		_agent.Verify(x => x.IsAnyPageController(It.IsAny<IControllerMetaData>()), Times.Once);

		_controllersExecutor.Verify(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController1) && t.RouteParameters == _routeParameters)),
			Times.Once);

		_controllersExecutor.Verify(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController2) && t.RouteParameters == null)),
			Times.Never);
	}

	[Test]
	public async Task ProcessControllers_StandardControllerMatchedReturnsRawData_ReturnedRawDataSubsequentNotExecuted()
	{
		// Arrange

		_agent.Setup(x => x.IsAnyPageController(It.IsAny<IControllerMetaData>())).Returns(false);
		_agent.Setup(x => x.GetStandardControllersMetaData()).Returns(() => new List<IControllerMetaData>
		{
			_metaData,
			_metaData
		});

		_controllersExecutor.Setup(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController1) && t.RouteParameters == _routeParameters)))
				.Returns(Task.FromResult(Web.Old.ControllerResponseResult.RawOutput));

		// Act
		var result = await _processor.ProcessControllers(null!, _context.Object);

		// Assert

		Assert.That(result, Is.EqualTo(ControllersProcessorResult.RawOutput));

		_controllersExecutor.Verify(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController1) && t.RouteParameters == _routeParameters)),
			Times.Once);
	}

	[Test]
	public async Task ProcessControllers_StandardControllerMatchedReturnsRedirect_ReturnedRedirectSubsequentNotExecuted()
	{
		// Arrange

		_agent.Setup(x => x.IsAnyPageController(It.IsAny<IControllerMetaData>())).Returns(false);
		_agent.Setup(x => x.GetStandardControllersMetaData()).Returns(() => new List<IControllerMetaData>
		{
			_metaData,
			_metaData
		});

		_controllersExecutor.Setup(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController1) && t.RouteParameters == _routeParameters)))
				.Returns(Task.FromResult(Web.Old.ControllerResponseResult.Redirect));

		// Act
		var result = await _processor.ProcessControllers(null!, _context.Object);

		// Assert

		Assert.That(result, Is.EqualTo(ControllersProcessorResult.Redirect));

		_controllersExecutor.Verify(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController1) && t.RouteParameters == _routeParameters)),
			Times.Once);
	}

	[Test]
	public async Task ProcessControllers_NotAuthenticated_ReturnedHttp401()
	{
		// Arrange
		_agent.Setup(x => x.IsSecurityRulesViolated(It.IsAny<IControllerMetaData>(), It.IsAny<ClaimsPrincipal>())).Returns(SecurityRuleCheckResult.NotAuthenticated);

		// Act
		var result = await _processor.ProcessControllers(null!, _context.Object);

		// Assert

		Assert.That(result, Is.EqualTo(ControllersProcessorResult.Http401));

		_agent.Setup(x => x.IsSecurityRulesViolated(It.IsAny<IControllerMetaData>(), It.IsAny<ClaimsPrincipal>()));
	}

	[Test]
	public async Task ProcessControllers_ForbiddenHave403Controller_403ControllerExecuted()
	{
		// Arrange

		_agent.Setup(x => x.IsSecurityRulesViolated(It.IsAny<IControllerMetaData>(), It.IsAny<ClaimsPrincipal>())).Returns(SecurityRuleCheckResult.Forbidden);
		_agent.Setup(x => x.GetHandlerController(It.Is<HandlerControllerType>(d => d == HandlerControllerType.Http403Handler)))
			.Returns(new ControllerMetaData(typeof(TestController2)));

		// Act
		var result = await _processor.ProcessControllers(null!, _context.Object);

		// Assert

		Assert.That(result, Is.EqualTo(ControllersProcessorResult.Ok));

		_controllersExecutor.Verify(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController1))),
			Times.Never);

		_controllersExecutor.Verify(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController2) && t.RouteParameters == null)));

		_redirector.VerifySet(x => x.PreviousPageUrl = It.IsAny<string>(), Times.Never);

		_agent.Verify(x => x.IsSecurityRulesViolated(It.IsAny<IControllerMetaData>(), It.IsAny<ClaimsPrincipal>()));
	}

	[Test]
	public async Task ProcessControllers_ForbiddenNotHave403Controller_Http403Returned()
	{
		// Arrange
		_agent.Setup(x => x.IsSecurityRulesViolated(It.IsAny<IControllerMetaData>(), It.IsAny<ClaimsPrincipal>())).Returns(SecurityRuleCheckResult.Forbidden);

		// Act
		var result = await _processor.ProcessControllers(null!, _context.Object);

		// Assert

		Assert.That(result, Is.EqualTo(ControllersProcessorResult.Http403));

		_controllersExecutor.Verify(x =>
			x.Execute(It.Is<IControllerExecutionArgs>(t => t.ControllerMetaData.ControllerType == typeof(TestController1))),
			Times.Never);

		_agent.Verify(x => x.IsSecurityRulesViolated(It.IsAny<IControllerMetaData>(), It.IsAny<ClaimsPrincipal>()));
	}
}