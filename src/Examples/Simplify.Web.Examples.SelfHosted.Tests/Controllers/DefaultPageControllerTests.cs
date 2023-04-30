using Moq;
using NUnit.Framework;
using Simplify.Web.Examples.SelfHosted.Controllers;
using Simplify.Web.Responses;

namespace Simplify.Web.Examples.SelfHosted.Tests.Controllers;

[TestFixture]
public class DefaultPageControllerTests
{
	[Test]
	public void Invoke_Default_MainContentSet()
	{
		// Assign
		var c = new Mock<DefaultController> { CallBase = true };

		// Act
		var result = c.Object.Invoke();

		// Assert
		Assert.AreEqual("Default", ((StaticTpl)result).TemplateFileName);
	}
}