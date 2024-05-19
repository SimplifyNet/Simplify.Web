using System.Net;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers;
using Simplify.Web.Controllers.Execution.WorkOrder;
using Simplify.Web.Controllers.Meta;

namespace Simplify.Web.Tests.Controllers.Execution.WorkOrder;

[TestFixture]
public class ExecutionWorkOrderBuilderTests
{
	[Test]
	public void Build()
	{
		// Arrange

		var builder = new ExecutionWorkOrderBuilder
		{
			HttpStatusCode = HttpStatusCode.BadRequest
		};

		builder.Controllers.Add(CreateController(3));
		builder.Controllers.Add(CreateController(1));
		builder.Controllers.Add(CreateController(5));
		builder.Controllers.Add(CreateController(2));
		builder.Controllers.Add(CreateController(4));

		// Act
		var result = builder.Build();

		// Assert

		Assert.That(result.HttpStatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

		Assert.That(result.Controllers.Count, Is.EqualTo(5));
		Assert.That(result.Controllers[0].Controller.ExecParameters!.RunPriority, Is.EqualTo(1));
		Assert.That(result.Controllers[1].Controller.ExecParameters!.RunPriority, Is.EqualTo(2));
		Assert.That(result.Controllers[2].Controller.ExecParameters!.RunPriority, Is.EqualTo(3));
		Assert.That(result.Controllers[3].Controller.ExecParameters!.RunPriority, Is.EqualTo(4));
		Assert.That(result.Controllers[4].Controller.ExecParameters!.RunPriority, Is.EqualTo(5));
	}

	private IMatchedController CreateController(int priority) =>
		Mock.Of<IMatchedController>(x => x.Controller.ExecParameters == new ControllerExecParameters(null, priority));
}