using System.Collections.Generic;
using System.Security.Claims;
using NUnit.Framework;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Security;
using Simplify.Web.Controllers.Security.Rules;

namespace Simplify.Web.Tests.Controllers.Security.Rules;

[TestFixture]
public class RoleAuthorizationRuleTests
{
	private readonly RoleAuthorizationRule _rule = new();

	[Test]
	public void ViolationStatus_IsForbidden() =>
		// Assert
		Assert.That(_rule.ViolationStatus, Is.EqualTo(SecurityStatus.Forbidden));

	[Test]
	public void IsViolated_AuthorizationRequiredWithGroupAuthorizedNoGroups_True()
	{
		// Arrange

		var security = new ControllerSecurity(true,
		[
			"Admin",
			"User"
		]);

		var claims = new List<Claim>
		{
			new(ClaimTypes.Name, "Foo")
		};

		var id = new ClaimsIdentity(claims, "test");
		var user = new ClaimsPrincipal(id);

		// Act
		var result = _rule.IsViolated(security, user);

		// Assert
		Assert.That(result, Is.True);
	}

	[Test]
	public void IsViolated_AuthorizationRequiredWithGroupAuthorizedNotInGroup_True()
	{
		// Arrange

		var security = new ControllerSecurity(true,
		[
			"Admin"
		]);

		var claims = new List<Claim>
		{
			new(ClaimTypes.Name, "Foo"),
			new(ClaimTypes.Role, "User")
		};

		var id = new ClaimsIdentity(claims, "test");
		var user = new ClaimsPrincipal(id);

		// Act
		var result = _rule.IsViolated(security, user);

		// Assert
		Assert.That(result, Is.True);
	}

	[Test]
	public void IsViolated_AuthorizationRequiredWithGroupAuthorizedInGroup_False()
	{
		// Arrange

		var security = new ControllerSecurity(true,
		[
			"Admin",
			"User"
		]);

		var claims = new List<Claim>
		{
			new(ClaimTypes.Name, "Foo"),
			new(ClaimTypes.Role, "User")
		};

		var id = new ClaimsIdentity(claims, "test");
		var user = new ClaimsPrincipal(id);

		// Act
		var result = _rule.IsViolated(security, user);

		// Assert
		Assert.That(result, Is.False);
	}
}