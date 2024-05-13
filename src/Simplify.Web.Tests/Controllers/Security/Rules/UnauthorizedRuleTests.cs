using System.Collections.Generic;
using System.Security.Claims;
using NUnit.Framework;
using Simplify.Web.Controllers.Security;
using Simplify.Web.Controllers.Security.Rules;

namespace Simplify.Web.Tests.Controllers.Resolution.Handling.Stages;

[TestFixture]
public class UnauthorizedRuleTests
{
	private readonly UnauthorizedRule _rule = new();

	[Test]
	public void ViolationStatus_IsUnauthorized() =>
		// Assert
		Assert.That(_rule.ViolationStatus, Is.EqualTo(SecurityStatus.Unauthorized));

	[Test]
	public void IsViolated_NotAuthorized_True()
	{
		// Act
		var result = _rule.IsViolated(null!, null);

		// Assert
		Assert.That(result, Is.True);
	}

	[Test]
	public void IsViolated_Authorized_False()
	{
		// Arrange

		var claims = new List<Claim>
		{
			new(ClaimTypes.Name, "Foo")
		};

		var id = new ClaimsIdentity(claims, "test");
		var user = new ClaimsPrincipal(id);

		// Act
		var result = _rule.IsViolated(null!, user);

		// Assert
		Assert.That(result, Is.False);
	}
}
