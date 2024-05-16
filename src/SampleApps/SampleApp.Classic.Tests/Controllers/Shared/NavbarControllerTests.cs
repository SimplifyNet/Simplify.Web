using Moq;
using NUnit.Framework;
using SampleApp.Classic.Controllers.Shared;
using Simplify.Templates;
using Simplify.Web.Responses;

namespace SampleApp.Classic.Tests.Controllers.Shared;

[TestFixture]
public class NavbarControllerTests
{
	[Test]
	public void Invoke_InlineTplNavbarDataDataReturned()
	{
		// Arrange

		var c = new Mock<NavbarController> { CallBase = true };

		c.Setup(x => x.TemplateFactory.Load(It.Is<string>(name => name == "Navbar"))).Returns(TemplateBuilder.FromString("Inline Data").Build);

		// Act
		var result = c.Object.Invoke() as InlineTpl;

		// Assert

		Assert.That(result, Is.Not.Null);
		Assert.That(result.DataCollectorVariableName, Is.EqualTo("Navbar"));
		Assert.That(result.Data, Is.EqualTo("Inline Data"));
	}
}