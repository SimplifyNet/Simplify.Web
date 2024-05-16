using System.Linq;
using NUnit.Framework;
using Simplify.Web.Attributes;

namespace Simplify.Web.Tests.Attributes;

[TestFixture]
public class AuthorizeAttributeTests
{
	[Test]
	public void Constructor_CommaSeparatedRoles_ParsedCorrectly()
	{
		// Act
		var attr = new AuthorizeAttribute("User, Admin");

		// Assert

		Assert.That(attr.RequiredUserRoles.Count(), Is.EqualTo(2));
		Assert.That(attr.RequiredUserRoles.Contains("Admin"), Is.True);
		Assert.That(attr.RequiredUserRoles.Contains("User"), Is.True);
	}

	[Test]
	public void Constructor_ParamsRoles_ParsedCorrectly()
	{
		// Act
		var attr = new AuthorizeAttribute("User", "Admin");

		// Assert

		Assert.That(attr.RequiredUserRoles.Count(), Is.EqualTo(2));
		Assert.That(attr.RequiredUserRoles.Contains("Admin"), Is.True);
		Assert.That(attr.RequiredUserRoles.Contains("User"), Is.True);
	}
}