using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.Resolution.State;

namespace Simplify.Web.Tests.Controllers.Resolution.State;

[TestFixture]
public class ControllerResolutionStateTests
{
	[Test]
	public void ToMatchedController_Controller_ParametersPassed()
	{
		// Arrange

		var md = Mock.Of<IControllerMetadata>();

		var state = new ControllerResolutionState(md)
		{
			RouteParameters = new Dictionary<string, object>()
		};

		// Act
		var mc = state.ToMatchedController();

		// Assert

		Assert.That(mc.Controller, Is.EqualTo(md));
		Assert.That(mc.RouteParameters, Is.EqualTo(state.RouteParameters));
	}
}