using Microsoft.Owin.Security;
using Moq;
using NUnit.Framework;
using Simplify.Web.Examples.SelfHosted.Controllers.Accounts;
using Simplify.Web.Modules;
using Simplify.Web.Responses;

namespace Simplify.Web.Examples.SelfHosted.Tests.Controllers.Accounts;

[TestFixture]
public class LogoutControllerTests
{
	[Test]
	public void Invoke_SignOutCalledRedirectedToDefaultPage()
	{
		// Assign

		var c = new Mock<LogoutController> { CallBase = true };
		var context = new Mock<IWebContext>();
		var auth = new Mock<IAuthenticationManager>();

		auth.Setup(x => x.SignOut());
		context.SetupGet(x => x.Context.Authentication).Returns(auth.Object);
		c.SetupGet(x => x.Context).Returns(context.Object);

		// Act
		var result = c.Object.Invoke();

		// Assert

		Assert.AreEqual(RedirectionType.DefaultPage, ((Redirect)result).RedirectionType);
		auth.Verify(x => x.SignOut());
	}
}