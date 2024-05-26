using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Simplify.Web.Controllers;
using Simplify.Web.Controllers.V2.Execution;
using Simplify.Web.Controllers.V2.Metadata;
using Simplify.Web.Responses;
using Simplify.Web.Tests.Controllers.V2.Execution.TestTypes;

namespace Simplify.Web.Tests.Controllers.V2.Execution;

[TestFixture]
public class Controller2ExecutorTests
{
	private Controller2Executor _executor = null!;

	private Mock<IController2Factory> _controllerFactory = null!;

	[SetUp]
	public void Initialize()
	{
		_controllerFactory = new Mock<IController2Factory>();

		_executor = new Controller2Executor(_controllerFactory.Object);
	}

	[Test]
	public async Task ExecuteAsync_VoidController_InvokedAndNull()
	{
		// Arrange

		var md = new Controller2Metadata(typeof(VoidController));
		var mc = new MatchedController(md);
		var controller = new VoidController();

		_controllerFactory.Setup(x => x.CreateController(It.Is<IMatchedController>(c => c == mc)))
			.Returns(controller);

		// Act
		var result = await _executor.ExecuteAsync(mc);

		// Assert

		Assert.That(result, Is.Null);
		Assert.That(controller.Invoked, Is.True);

		_controllerFactory.Verify(x => x.CreateController(It.Is<IMatchedController>(c => c == mc)));
	}

	[Test]
	public async Task ExecuteAsync_TaskResultController_InvokedAndNull()
	{
		// Arrange

		var md = new Controller2Metadata(typeof(TaskResultController));
		var mc = new MatchedController(md);
		var controller = new TaskResultController();

		_controllerFactory.Setup(x => x.CreateController(It.Is<IMatchedController>(c => c == mc)))
			.Returns(controller);

		// Act
		var result = await _executor.ExecuteAsync(mc);

		// Assert

		Assert.That(result, Is.Null);
		Assert.That(controller.Invoked, Is.True);

		_controllerFactory.Verify(x => x.CreateController(It.Is<IMatchedController>(c => c == mc)));
	}

	[Test]
	public async Task ExecuteAsync_NullResponseController_InvokedAndNull()
	{
		// Arrange

		var md = new Controller2Metadata(typeof(NullResponseController));
		var mc = new MatchedController(md);
		var controller = new NullResponseController();

		_controllerFactory.Setup(x => x.CreateController(It.Is<IMatchedController>(c => c == mc)))
			.Returns(controller);

		// Act
		var result = await _executor.ExecuteAsync(mc);

		// Assert

		Assert.That(result, Is.Null);
		Assert.That(controller.Invoked, Is.True);

		_controllerFactory.Verify(x => x.CreateController(It.Is<IMatchedController>(c => c == mc)));
	}

	[Test]
	public async Task ExecuteAsync_StringResponseController_InvokedAndContentResponseReturned()
	{
		// Arrange

		var md = new Controller2Metadata(typeof(StringResponseController));
		var mc = new MatchedController(md);
		var controller = new StringResponseController();

		_controllerFactory.Setup(x => x.CreateController(It.Is<IMatchedController>(c => c == mc)))
			.Returns(controller);

		// Act
		var result = await _executor.ExecuteAsync(mc);

		// Assert

		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.TypeOf(typeof(Content)));
		Assert.That(controller.Invoked, Is.True);
		Assert.That(((Content)result!).StringContent, Is.EqualTo("Foo"));

		_controllerFactory.Verify(x => x.CreateController(It.Is<IMatchedController>(c => c == mc)));
	}

	[Test]
	public async Task ExecuteAsync_TaskStringResponseController_InvokedAndContentResponseReturned()
	{
		// Arrange

		var md = new Controller2Metadata(typeof(TaskStringResponseController));
		var mc = new MatchedController(md);
		var controller = new TaskStringResponseController();

		_controllerFactory.Setup(x => x.CreateController(It.Is<IMatchedController>(c => c == mc)))
			.Returns(controller);

		// Act
		var result = await _executor.ExecuteAsync(mc);

		// Assert

		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.TypeOf(typeof(Content)));
		Assert.That(controller.Invoked, Is.True);
		Assert.That(((Content)result!).StringContent, Is.EqualTo("Foo"));

		_controllerFactory.Verify(x => x.CreateController(It.Is<IMatchedController>(c => c == mc)));
	}

	[Test]
	public async Task ExecuteAsync_ModelController_InvokedAndNull()
	{
		// Arrange

		var md = new Controller2Metadata(typeof(ModelController));
		var mc = new MatchedController(md);
		var controller = new ModelController();

		_controllerFactory.Setup(x => x.CreateController(It.Is<IMatchedController>(c => c == mc)))
			.Returns(controller);

		// Act
		var result = await _executor.ExecuteAsync(mc);

		// Assert

		Assert.That(result, Is.Null);
		Assert.That(controller.Invoked, Is.True);

		_controllerFactory.Verify(x => x.CreateController(It.Is<IMatchedController>(c => c == mc)));
	}

	[Test]
	public async Task ExecuteAsync_AllParamsController_InvokedAndContentResponseReturned()
	{
		// Arrange

		var parameters = new Dictionary<string, object>
		{
			// ReSharper disable StringLiteralTypo
			{"decimalparam", 15.3m},
			{"intarrayparam", new[]{6,3,8}},
			{"boolarrayparam", new[]{false,true}},
			{ "boolparam", true },
			{ "intparam", 2},
			{ "stringparam", "foo" },
			{ "stringarrayparam", new[]{"foo","bar"} },
			{ "decimalarrayparam", new List<decimal>{5.8m,7.1m}.ToArray() }
			// ReSharper restore StringLiteralTypo
		};

		var md = new Controller2Metadata(typeof(AllParamsController));
		var mc = new MatchedController(md, parameters);
		var controller = new AllParamsController();

		_controllerFactory.Setup(x => x.CreateController(It.Is<IMatchedController>(c => c == mc)))
			.Returns(controller);

		// Act
		var result = await _executor.ExecuteAsync(mc);

		// Assert

		Assert.That(result, Is.Null);

		Assert.That(controller.StringParam, Is.EqualTo("foo"));
		Assert.That(controller.IntParam, Is.EqualTo(2));
		Assert.That(controller.DecimalParam, Is.EqualTo(15.3m));
		Assert.That(controller.BoolParam, Is.EqualTo(true));

		Assert.That(controller.BoolArrayParam.Length, Is.EqualTo(2));
		Assert.That(controller.BoolArrayParam[0], Is.EqualTo(false));
		Assert.That(controller.BoolArrayParam[1], Is.EqualTo(true));

		Assert.That(controller.IntArrayParam.Length, Is.EqualTo(3));
		Assert.That(controller.IntArrayParam[0], Is.EqualTo(6));
		Assert.That(controller.IntArrayParam[1], Is.EqualTo(3));
		Assert.That(controller.IntArrayParam[2], Is.EqualTo(8));

		Assert.That(controller.StringArrayParam.Length, Is.EqualTo(2));
		Assert.That(controller.StringArrayParam[0], Is.EqualTo("foo"));
		Assert.That(controller.StringArrayParam[1], Is.EqualTo("bar"));

		Assert.That(controller.DecimalArrayParam.Length, Is.EqualTo(2));
		Assert.That(controller.DecimalArrayParam[0], Is.EqualTo(5.8m));
		Assert.That(controller.DecimalArrayParam[1], Is.EqualTo(7.1m));

		_controllerFactory.Verify(x => x.CreateController(It.Is<IMatchedController>(c => c == mc)));
	}
}