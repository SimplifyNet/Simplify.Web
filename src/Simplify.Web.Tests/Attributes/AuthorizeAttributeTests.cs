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

		Assert.AreEqual(2, attr.RequiredUserRoles.Count());
		Assert.IsTrue(attr.RequiredUserRoles.Contains("Admin"));
		Assert.IsTrue(attr.RequiredUserRoles.Contains("User"));
	}

	[Test]
	public void Constructor_ParamsRoles_ParsedCorrectly()
	{
		// Act
		var attr = new AuthorizeAttribute("User", "Admin");

		// Assert

		Assert.AreEqual(2, attr.RequiredUserRoles.Count());
		Assert.IsTrue(attr.RequiredUserRoles.Contains("Admin"));
		Assert.IsTrue(attr.RequiredUserRoles.Contains("User"));
	}
}