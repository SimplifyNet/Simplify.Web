using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Resolution.Stages;
using Simplify.Web.Controllers.Resolution.State;
using Simplify.Web.Controllers.Security;

namespace Simplify.Web.Tests.Controllers.Resolution.Stages;

[TestFixture]
public class SecurityCheckStageTests
{
	private SecurityCheckStage _stage = null!;
	private Mock<ISecurityChecker> _securityChecker = null!;

	[SetUp]
	public void Initialize()
	{
		_securityChecker = new Mock<ISecurityChecker>();

		_stage = new SecurityCheckStage(_securityChecker.Object);
	}

	[Test]
	public void Execute_SecurityResultIsOk_StateResultSetAndExecutionIsNotStopped()
	{
		// Arrange

		var metadata = Mock.Of<IControllerMetadata>();
		var state = Mock.Of<IControllerResolutionState>(x => x.Controller == metadata);
		var user = new ClaimsPrincipal();
		var context = Mock.Of<HttpContext>(x => x.User == user);
		var stopExecution = new Mock<Action>();

		// Act
		_stage.Execute(state, context, stopExecution.Object);

		// Assert

		Assert.That(state.SecurityStatus, Is.EqualTo(SecurityStatus.Ok));

		stopExecution.Verify(x => x.Invoke(), Times.Never);
		_securityChecker.Verify(x => x.CheckSecurityRules(It.Is<IControllerMetadata>(m => m == metadata), It.Is<ClaimsPrincipal>(c => c == user)));
	}

	[Test]
	public void Execute_SecurityResultIsNotOk_StateResultSetAndExecutionIsStopped()
	{
		// Arrange

		var metadata = Mock.Of<IControllerMetadata>();
		var state = Mock.Of<IControllerResolutionState>(x => x.Controller == metadata);
		var user = new ClaimsPrincipal();
		var context = Mock.Of<HttpContext>(x => x.User == user);
		var stopExecution = new Mock<Action>();

		_securityChecker.Setup(x => x.CheckSecurityRules(It.Is<IControllerMetadata>(m => m == metadata), It.Is<ClaimsPrincipal>(c => c == user)))
			.Returns(SecurityStatus.Forbidden);


		// Act
		_stage.Execute(state, context, stopExecution.Object);

		// Assert

		Assert.That(state.SecurityStatus, Is.EqualTo(SecurityStatus.Forbidden));

		stopExecution.Verify(x => x.Invoke());
		_securityChecker.Verify(x => x.CheckSecurityRules(It.Is<IControllerMetadata>(m => m == metadata), It.Is<ClaimsPrincipal>(c => c == user)));
	}
}
