#nullable disable

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.DI;
using Simplify.Web.Core.Controllers;
using Simplify.Web.Core.Controllers.Execution;
using Simplify.Web.Meta;
using Simplify.Web.Modules;
using Simplify.Web.Routing;
using Simplify.Web.Tests.TestEntities;

namespace Simplify.Web.Tests.Core.Controllers
{
	[TestFixture]
	public class ControllersProcessorTests
	{
		private readonly IDIContainerProvider _containerProvider = null;
		private readonly IDictionary<string, object> _routeParameters = new ExpandoObject();
		private ControllersProcessor _processor;
		private Mock<IControllersAgent> _agent;
		private Mock<IControllerExecutor> _controllersExecutor;
		private Mock<IRedirector> _redirector;
		private Mock<HttpContext> _context;

		private ControllerMetaData _metaData;

		[SetUp]
		public void Initialize()
		{
			_agent = new Mock<IControllersAgent>();
			_controllersExecutor = new Mock<IControllerExecutor>();
			_redirector = new Mock<IRedirector>();
			_processor = new ControllersProcessor(_agent.Object, _controllersExecutor.Object, _redirector.Object);

			_context = new Mock<HttpContext>();

			_metaData = new ControllerMetaData(typeof(TestController1),
				new ControllerExecParameters(new Dictionary<HttpMethod, string> { { HttpMethod.Put, "/foo/bar" } }));

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
			// Assign
			_agent.Setup(x => x.MatchControllerRoute(It.IsAny<IControllerMetaData>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new RouteMatchResult());

			// Act
			var result = await _processor.ProcessControllers(_containerProvider, _context.Object);

			// Assert

			Assert.AreEqual(ControllersProcessorResult.Http404, result);
			_controllersExecutor.Verify(
				x =>
					x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController1)), It.IsAny<IDIContainerProvider>(),
						It.IsAny<HttpContext>(), It.IsAny<IDictionary<string, Object>>()), Times.Never);
		}

		[Test]
		public async Task ProcessControllers_NoControllersMatchedButHave404Controller_404ControllerExecuted()
		{
			// Assign

			_agent.Setup(x => x.MatchControllerRoute(It.IsAny<IControllerMetaData>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new RouteMatchResult());
			_agent.Setup(
				x => x.GetHandlerController(It.Is<HandlerControllerType>(d => d == HandlerControllerType.Http404Handler)))
				.Returns(new ControllerMetaData(typeof(TestController2)));

			// Act
			var result = await _processor.ProcessControllers(_containerProvider, _context.Object);

			// Assert

			Assert.AreEqual(ControllersProcessorResult.Ok, result);
			_controllersExecutor.Verify(x =>
				x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController1)), It.IsAny<IDIContainerProvider>(),
					It.IsAny<HttpContext>(), It.IsAny<IDictionary<string, Object>>()), Times.Never);

			_controllersExecutor.Verify(x =>
				x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController2)), It.IsAny<IDIContainerProvider>(),
					It.IsAny<HttpContext>(), It.Is<IDictionary<string, Object>>(d => d == null)));

			_redirector.VerifySet(x => x.PreviousPageUrl = It.IsAny<string>(), Times.Never);
		}

		[Test]
		public async Task ProcessControllers_NoControllersMatchedButHave404ControllerRawResult_404ControllerExecutedRawReturned()
		{
			// Assign

			_agent.Setup(x => x.MatchControllerRoute(It.IsAny<IControllerMetaData>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new RouteMatchResult());
			_agent.Setup(
				x => x.GetHandlerController(It.Is<HandlerControllerType>(d => d == HandlerControllerType.Http404Handler)))
				.Returns(new ControllerMetaData(typeof(TestController2)));

			_controllersExecutor.Setup(
				x =>
					x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController2)), It.IsAny<IDIContainerProvider>(),
						It.IsAny<HttpContext>(), It.IsAny<IDictionary<string, Object>>())).Returns(Task.FromResult(ControllerResponseResult.RawOutput));

			// Act
			var result = await _processor.ProcessControllers(_containerProvider, _context.Object);

			// Assert

			Assert.AreEqual(ControllersProcessorResult.RawOutput, result);
			_controllersExecutor.Verify(x =>
				x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController1)), It.IsAny<IDIContainerProvider>(),
					It.IsAny<HttpContext>(), It.IsAny<IDictionary<string, Object>>()), Times.Never);

			_controllersExecutor.Verify(x =>
				x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController2)), It.IsAny<IDIContainerProvider>(),
					It.IsAny<HttpContext>(), It.Is<IDictionary<string, Object>>(d => d == null)));
		}

		[Test]
		public async Task ProcessControllers_NoControllersMatchedButHave404ControllerRedirect_404ControllerExecutedRedirectReturned()
		{
			// Assign

			_agent.Setup(x => x.MatchControllerRoute(It.IsAny<IControllerMetaData>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new RouteMatchResult());
			_agent.Setup(
				x => x.GetHandlerController(It.Is<HandlerControllerType>(d => d == HandlerControllerType.Http404Handler)))
				.Returns(new ControllerMetaData(typeof(TestController2)));

			_controllersExecutor.Setup(
				x =>
					x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController2)), It.IsAny<IDIContainerProvider>(),
						It.IsAny<HttpContext>(), It.IsAny<IDictionary<string, Object>>())).Returns(Task.FromResult(ControllerResponseResult.Redirect));

			// Act
			var result = await _processor.ProcessControllers(_containerProvider, _context.Object);

			// Assert

			Assert.AreEqual(ControllersProcessorResult.Redirect, result);
			_controllersExecutor.Verify(x =>
				x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController1)), It.IsAny<IDIContainerProvider>(),
					It.IsAny<HttpContext>(), It.IsAny<IDictionary<string, Object>>()), Times.Never);

			_controllersExecutor.Verify(x =>
				x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController2)), It.IsAny<IDIContainerProvider>(),
					It.IsAny<HttpContext>(), It.Is<IDictionary<string, Object>>(d => d == null)));
		}

		[Test]
		public async Task ProcessControllers_OnlyAnyPageControllerMatchedButHave404Controller_404ControllerExecuted()
		{
			// Assign

			_agent.Setup(x => x.MatchControllerRoute(It.IsAny<IControllerMetaData>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new RouteMatchResult(true));
			_agent.Setup(
				x => x.GetHandlerController(It.Is<HandlerControllerType>(d => d == HandlerControllerType.Http404Handler)))
				.Returns(new ControllerMetaData(typeof(TestController2)));
			_agent.Setup(x => x.IsAnyPageController(It.IsAny<IControllerMetaData>())).Returns(true);

			// Act
			var result = await _processor.ProcessControllers(_containerProvider, _context.Object);

			// Assert

			Assert.AreEqual(ControllersProcessorResult.Ok, result);
			_agent.Verify(x => x.IsAnyPageController(It.IsAny<IControllerMetaData>()));
			_controllersExecutor.Verify(x =>
				x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController1)), It.IsAny<IDIContainerProvider>(),
					It.IsAny<HttpContext>(), It.IsAny<IDictionary<string, Object>>()));

			_controllersExecutor.Verify(
				x =>
					x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController2)), It.IsAny<IDIContainerProvider>(),
						It.IsAny<HttpContext>(), It.Is<IDictionary<string, Object>>(d => d == null)));
		}

		[Test]
		public async Task ProcessControllers_StandardControllerMatched_Executed()
		{
			// Assign
			_agent.Setup(x => x.IsAnyPageController(It.IsAny<IControllerMetaData>())).Returns(false);

			// Act
			var result = await _processor.ProcessControllers(_containerProvider, _context.Object);

			// Assert

			Assert.AreEqual(ControllersProcessorResult.Ok, result);

			_agent.Verify(x => x.IsAnyPageController(It.IsAny<IControllerMetaData>()));

			_controllersExecutor.Verify(
				x =>
					x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController1)), It.IsAny<IDIContainerProvider>(),
						It.IsAny<HttpContext>(), It.Is<IDictionary<string, Object>>(d => d == _routeParameters)));

			_controllersExecutor.Verify(
				x =>
					x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController2)), It.IsAny<IDIContainerProvider>(),
						It.IsAny<HttpContext>(), It.Is<IDictionary<string, Object>>(d => d == null)), Times.Never);

			_redirector.Verify(x => x.SetPreviousPageUrlToCurrentPage());

			// Check
			//_controllersExecutor.Verify(x => x.ProcessAsyncControllersResponses(It.IsAny<IDIContainerProvider>()));
		}

		[Test]
		public async Task ProcessControllers_StandardControllersOneIsMatchedNull_ExecutedOnce()
		{
			// Assign

			_agent.Setup(x => x.IsAnyPageController(It.IsAny<IControllerMetaData>())).Returns(false);
			_agent.SetupSequence(
				x => x.MatchControllerRoute(It.IsAny<IControllerMetaData>(), It.IsAny<string>(), It.IsAny<string>()))
				.Returns(new RouteMatchResult(true, _routeParameters))
				.Returns((IRouteMatchResult)null);
			_agent.Setup(x => x.GetStandardControllersMetaData()).Returns(() => new List<IControllerMetaData>
			{
				_metaData,
				_metaData
			});

			// Act
			var result = await _processor.ProcessControllers(_containerProvider, _context.Object);

			// Assert

			Assert.AreEqual(ControllersProcessorResult.Ok, result);
			_agent.Verify(x => x.IsAnyPageController(It.IsAny<IControllerMetaData>()), Times.Once);
			_controllersExecutor.Verify(
				x =>
					x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController1)), It.IsAny<IDIContainerProvider>(),
						It.IsAny<HttpContext>(), It.Is<IDictionary<string, Object>>(d => d == _routeParameters)), Times.Once);

			_controllersExecutor.Verify(
				x =>
					x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController2)), It.IsAny<IDIContainerProvider>(),
						It.IsAny<HttpContext>(), It.Is<IDictionary<string, Object>>(d => d == null)), Times.Never);

			// Check
			//_controllersExecutor.Verify(x => x.ProcessAsyncControllersResponses(It.IsAny<IDIContainerProvider>()), Times.Once);
		}

		[Test]
		public async Task ProcessControllers_StandardControllerMatchedReturnsRawData_ReturnedRawDataSubsequentNotExecuted()
		{
			// Assign

			_agent.Setup(x => x.IsAnyPageController(It.IsAny<IControllerMetaData>())).Returns(false);
			_agent.Setup(x => x.GetStandardControllersMetaData()).Returns(() => new List<IControllerMetaData>
			{
				_metaData,
				_metaData
			});

			_controllersExecutor.Setup(
				x =>
					x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController1)), It.IsAny<IDIContainerProvider>(),
						It.IsAny<HttpContext>(), It.Is<IDictionary<string, Object>>(d => d == _routeParameters))).Returns(
				Task.FromResult(ControllerResponseResult.RawOutput));

			// Act
			var result = await _processor.ProcessControllers(_containerProvider, _context.Object);

			// Assert

			Assert.AreEqual(ControllersProcessorResult.RawOutput, result);
			_controllersExecutor.Verify(
				x =>
					x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController1)), It.IsAny<IDIContainerProvider>(),
						It.IsAny<HttpContext>(), It.Is<IDictionary<string, Object>>(d => d == _routeParameters)), Times.Once);
		}

		[Test]
		public async Task ProcessControllers_StandardControllerMatchedReturnsRedirect_ReturnedRedirectSubsequentNotExecuted()
		{
			// Assign

			_agent.Setup(x => x.IsAnyPageController(It.IsAny<IControllerMetaData>())).Returns(false);
			_agent.Setup(x => x.GetStandardControllersMetaData()).Returns(() => new List<IControllerMetaData>
			{
				_metaData,
				_metaData
			});

			_controllersExecutor.Setup(
				x =>
					x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController1)), It.IsAny<IDIContainerProvider>(),
						It.IsAny<HttpContext>(), It.Is<IDictionary<string, Object>>(d => d == _routeParameters))).Returns(
				Task.FromResult(ControllerResponseResult.Redirect));

			// Act
			var result = await _processor.ProcessControllers(_containerProvider, _context.Object);

			// Assert

			Assert.AreEqual(ControllersProcessorResult.Redirect, result);
			_controllersExecutor.Verify(
				x =>
					x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController1)), It.IsAny<IDIContainerProvider>(),
						It.IsAny<HttpContext>(), It.Is<IDictionary<string, Object>>(d => d == _routeParameters)), Times.Once);
		}

		[Test]
		public async Task ProcessControllers_NotAuthenticated_ReturnedHttp401()
		{
			// Assign
			_agent.Setup(x => x.IsSecurityRulesViolated(It.IsAny<IControllerMetaData>(), It.IsAny<ClaimsPrincipal>())).Returns(SecurityRuleCheckResult.NotAuthenticated);

			// Act
			var result = await _processor.ProcessControllers(_containerProvider, _context.Object);

			// Assert

			Assert.AreEqual(ControllersProcessorResult.Http401, result);
			_agent.Setup(x => x.IsSecurityRulesViolated(It.IsAny<IControllerMetaData>(), It.IsAny<ClaimsPrincipal>()));
		}

		[Test]
		public async Task ProcessControllers_ForbiddenHave403Controller_403ControllerExecuted()
		{
			// Assign

			_agent.Setup(x => x.IsSecurityRulesViolated(It.IsAny<IControllerMetaData>(), It.IsAny<ClaimsPrincipal>())).Returns(SecurityRuleCheckResult.Forbidden);
			_agent.Setup(
				x => x.GetHandlerController(It.Is<HandlerControllerType>(d => d == HandlerControllerType.Http403Handler)))
				.Returns(new ControllerMetaData(typeof(TestController2)));

			// Act
			var result = await _processor.ProcessControllers(_containerProvider, _context.Object);

			// Assert

			Assert.AreEqual(ControllersProcessorResult.Ok, result);
			_controllersExecutor.Verify(x =>
				x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController1)), It.IsAny<IDIContainerProvider>(),
					It.IsAny<HttpContext>(), It.IsAny<IDictionary<string, Object>>()), Times.Never);
			_controllersExecutor.Verify(
				x =>
					x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController2)), It.IsAny<IDIContainerProvider>(),
						It.IsAny<HttpContext>(), It.Is<IDictionary<string, Object>>(d => d == null)));
			_agent.Setup(x => x.IsSecurityRulesViolated(It.IsAny<IControllerMetaData>(), It.IsAny<ClaimsPrincipal>()));

			_redirector.VerifySet(x => x.PreviousPageUrl = It.IsAny<string>(), Times.Never);
		}

		[Test]
		public async Task ProcessControllers_ForbiddenNotHave403Controller_Http403Returned()
		{
			// Assign
			_agent.Setup(x => x.IsSecurityRulesViolated(It.IsAny<IControllerMetaData>(), It.IsAny<ClaimsPrincipal>())).Returns(SecurityRuleCheckResult.Forbidden);

			// Act
			var result = await _processor.ProcessControllers(_containerProvider, _context.Object);

			// Assert

			Assert.AreEqual(ControllersProcessorResult.Http403, result);
			_controllersExecutor.Verify(x =>
				x.Execute(It.Is<IControllerMetaData>(t => t.ControllerType == typeof(TestController1)), It.IsAny<IDIContainerProvider>(),
					It.IsAny<HttpContext>(), It.IsAny<IDictionary<string, Object>>()), Times.Never);
			_agent.Setup(x => x.IsSecurityRulesViolated(It.IsAny<IControllerMetaData>(), It.IsAny<ClaimsPrincipal>()));
		}
	}
}