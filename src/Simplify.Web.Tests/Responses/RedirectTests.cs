using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Simplify.Web.Modules;
using Simplify.Web.Responses;

namespace Simplify.Web.Tests.Responses;

[TestFixture]
public class RedirectTests
{
	private Mock<IRedirector> _redirector = null!;

	[SetUp]
	public void Initialize()
	{
		_redirector = new Mock<IRedirector>();
	}

	[Test]
	public async Task Process_RedirectWithUrl_RedirectorRedirectCalled()
	{
		// Assign

		var redirect = new Mock<Redirect>("foo") { CallBase = true };
		redirect.SetupGet(x => x.Redirector).Returns(_redirector.Object);

		// Act
		var result = await redirect.Object.Process();

		// Assert

		_redirector.Verify(x => x.Redirect(It.Is<string>(d => d == "foo")));
		Assert.AreEqual(ControllerResponseResult.Redirect, result);
	}

	[Test]
	public async Task Process_RedirectWitRedirectionType_RedirectorRedirectWithRedirectionTypeCalled()
	{
		// Assign

		var redirect = new Mock<Redirect>(RedirectionType.PreviousPageWithBookmark, "test") { CallBase = true };
		redirect.SetupGet(x => x.Redirector).Returns(_redirector.Object);

		// Act
		var result = await redirect.Object.Process();

		// Assert

		_redirector.Verify(x => x.Redirect(It.Is<RedirectionType>(d => d == RedirectionType.PreviousPageWithBookmark), It.Is<string>(d => d == "test")));
		Assert.AreEqual(ControllerResponseResult.Redirect, result);
	}
}