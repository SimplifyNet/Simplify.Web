using System.Security.Claims;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Security;

namespace Simplify.Web.Tests.Controllers.Security;

[TestFixture]
public class SecurityCheckerTests
{
	[Test]
	public void CheckSecurityRules_MetadataSecurityIsNull_OkNoRulesCalled()
	{
		// Arrange

		var rule1 = new Mock<ISecurityRule>();
		var rule2 = new Mock<ISecurityRule>();
		var rule3 = new Mock<ISecurityRule>();
		var metaData = Mock.Of<IControllerMetadata>();

		var pipeline = new SecurityChecker([rule1.Object, rule2.Object, rule3.Object]);

		// Act
		var result = pipeline.CheckSecurityRules(metaData, null!);

		// Asset

		Assert.That(result, Is.EqualTo(SecurityStatus.Ok));

		rule1.Verify(x => x.IsViolated(It.IsAny<ControllerSecurity>(), It.IsAny<ClaimsPrincipal>()), Times.Never);
		rule2.Verify(x => x.IsViolated(It.IsAny<ControllerSecurity>(), It.IsAny<ClaimsPrincipal>()), Times.Never);
		rule3.Verify(x => x.IsViolated(It.IsAny<ControllerSecurity>(), It.IsAny<ClaimsPrincipal>()), Times.Never);
	}

	[Test]
	public void CheckSecurityRules_IsAuthorizationRequiredSecondRuleIsViolated_FirstTwoCalledSecondStatusReturned()
	{
		// Arrange

		var rule1 = new Mock<ISecurityRule>();
		var rule2 = new Mock<ISecurityRule>();
		var rule3 = new Mock<ISecurityRule>();
		var metaData = Mock.Of<IControllerMetadata>(x => x.Security == new ControllerSecurity(true, null));

		rule2.SetupGet(x => x.ViolationStatus).Returns(SecurityStatus.Forbidden);
		rule2.Setup(x => x.IsViolated(It.IsAny<ControllerSecurity>(), It.IsAny<ClaimsPrincipal>())).Returns(true);

		var pipeline = new SecurityChecker([rule1.Object, rule2.Object, rule3.Object]);

		// Act
		var result = pipeline.CheckSecurityRules(metaData, null!);

		// Asset

		Assert.That(result, Is.EqualTo(SecurityStatus.Forbidden));

		rule1.Verify(x => x.IsViolated(It.IsAny<ControllerSecurity>(), It.IsAny<ClaimsPrincipal>()));
		rule2.Verify(x => x.IsViolated(It.IsAny<ControllerSecurity>(), It.IsAny<ClaimsPrincipal>()));
		rule3.Verify(x => x.IsViolated(It.IsAny<ControllerSecurity>(), It.IsAny<ClaimsPrincipal>()), Times.Never);
	}
}