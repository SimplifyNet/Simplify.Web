using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Resolution.Stages;
using Simplify.Web.Controllers.Resolution.State;
using Simplify.Web.Controllers.RouteMatching;
using Simplify.Web.Controllers.RouteMatching.Resolver;
using Simplify.Web.Http;

namespace Simplify.Web.Tests.Controllers.Resolution.Stages;

[TestFixture]
public class RouteMatchingStageTests
{
	private RouteMatchingStage _stage = null!;
	private Mock<IRouteMatcherResolver> _routeMatcherResolver = null!;

	[SetUp]
	public void Initialize()
	{
		_routeMatcherResolver = new Mock<IRouteMatcherResolver>();

		_stage = new RouteMatchingStage(_routeMatcherResolver.Object);
	}

	[Test]
	public void Execute_NoRespectiveMethod_ExecutionStoppedAndNoParametersSet()
	{
		// Arrange

		var state = Mock.Of<IControllerResolutionState>(x => x.Controller.ExecParameters == new ControllerExecParameters(null, 0));
		var context = Mock.Of<HttpContext>();
		var stopExecution = new Mock<Action>();

		// Act
		_stage.Execute(state, context, stopExecution.Object);

		// Assert

		stopExecution.Verify(x => x.Invoke());
		_routeMatcherResolver.Verify(x => x.Resolve(It.IsAny<IControllerMetadata>()), Times.Never);
	}

	[Test]
	public void Execute_MatchedMethodButNotMatchedRoute_ExecutionStoppedAndIsMatchedSetAndNoRouteParametersSet()
	{
		// Arrange

		var controllerPath = "/foo";
		var currentPath = "/bar";

		var state = Mock.Of<IControllerResolutionState>(x =>
			x.Controller.ExecParameters == new ControllerExecParameters(
				new Dictionary<HttpMethod, string> {
				{
					HttpMethod.Post, controllerPath }
				}, 0));

		var context = Mock.Of<HttpContext>(x =>
			x.Request.Method == Relations.HttpMethodToToHttpMethodStringRelation[HttpMethod.Post] &&
			x.Request.Path == new PathString(currentPath));

		var stopExecution = new Mock<Action>();
		var routeMatcher = new Mock<IRouteMatcher>();

		_routeMatcherResolver.Setup(x => x.Resolve(It.IsAny<IControllerMetadata>())).Returns(routeMatcher.Object);

		routeMatcher.Setup(x => x.Match(It.Is<string>(s => s == currentPath), It.Is<string>(s => s == controllerPath)))
			.Returns(Mock.Of<IRouteMatchResult>(x =>
				x.Success == false));
		// Act
		_stage.Execute(state, context, stopExecution.Object);

		// Assert

		Assert.That(state.IsMatched, Is.False);
		Assert.That(state.RouteParameters, Is.Null);

		stopExecution.Verify(x => x.Invoke());
		_routeMatcherResolver.Verify(x => x.Resolve(It.IsAny<IControllerMetadata>()));
		routeMatcher.Verify(x => x.Match(It.Is<string>(s => s == currentPath), It.Is<string>(s => s == controllerPath)));
	}

	[Test]
	public void Execute_MatchedMethodAndMatchedRoute_IsMatchedAndRouteParametersAreSet()
	{
		// Arrange

		var controllerPath = "/foo";
		var currentPath = "/foo";

		var state = Mock.Of<IControllerResolutionState>(x =>
			x.Controller.ExecParameters == new ControllerExecParameters(
				new Dictionary<HttpMethod, string> {
				{
					HttpMethod.Post, controllerPath }
				}, 0));

		var context = Mock.Of<HttpContext>(x =>
			x.Request.Method == Relations.HttpMethodToToHttpMethodStringRelation[HttpMethod.Post] &&
			x.Request.Path == new PathString(currentPath));

		var stopExecution = new Mock<Action>();
		var routeMatcher = new Mock<IRouteMatcher>();

		_routeMatcherResolver.Setup(x => x.Resolve(It.IsAny<IControllerMetadata>())).Returns(routeMatcher.Object);

		routeMatcher.Setup(x => x.Match(It.Is<string>(s => s == currentPath), It.Is<string>(s => s == controllerPath)))
			.Returns(Mock.Of<IRouteMatchResult>(x =>
				x.Success == true &&
			 	x.RouteParameters == new Dictionary<string, object>()));

		// Act
		_stage.Execute(state, context, stopExecution.Object);

		// Assert

		Assert.That(state.IsMatched, Is.True);
		Assert.That(state.RouteParameters, Is.Not.Null);

		stopExecution.Verify(x => x.Invoke(), Times.Never);
		_routeMatcherResolver.Verify(x => x.Resolve(It.IsAny<IControllerMetadata>()));
		routeMatcher.Verify(x => x.Match(It.Is<string>(s => s == currentPath), It.Is<string>(s => s == controllerPath)));
	}
}
