using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Simplify.DI;
using Simplify.DI.Provider.DryIoc;
using Simplify.Web.Model;
using Simplify.Web.Model.Binding;
using Simplify.Web.Model.Validation;
using Simplify.Web.Modules.Context;
using Simplify.Web.Tests.Model.Binding.Binders.TestTypes;

namespace Simplify.Web.Tests.Model;

[TestFixture]
public class HttpModelHandlerTests
{
	[Test]
	public void ProcessAsync_NotMatchedBinder_NotProcessed()
	{
		// Arrange

		HttpModelHandler.ModelBindersTypes.Clear();

		var context = Mock.Of<IWebContext>(x => x.Request.ContentType == "Foo");
		var provider = new DryIocDIProvider();

		// Act & Assert

		using var scope = provider.BeginLifetimeScope();
		var handler = new HttpModelHandler(scope.Resolver, context);

		Assert.ThrowsAsync<ModelBindingException>(handler.ProcessAsync<FooModel>);
	}

	[Test]
	public async Task ProcessAsync_MatchedBinder_BoundAndValidated()
	{
		// Arrange

		HttpModelHandler.ModelBindersTypes.Clear();
		HttpModelHandler.ModelValidatorsTypes.Clear();

		var originalModel = new FooModel { ID = 1, Name = "Bar" };
		var binder = new Mock<IModelBinder>();
		var validator = new Mock<IModelValidator>();
		var context = Mock.Of<IWebContext>(x => x.Request.ContentType == "Foo");
		var provider = new DryIocDIProvider();

		binder.Setup(x => x.BindAsync(It.IsAny<ModelBinderEventArgs<FooModel>>()))
			.Callback<ModelBinderEventArgs<FooModel>>((args) => args
			.SetModel(originalModel));

		HttpModelHandler.RegisterModelBinder<IModelBinder>();
		HttpModelHandler.RegisterModelValidator<IModelValidator>();

		provider.Register(r => binder.Object);
		provider.Register(r => validator.Object);

		// Act

		using var scope = provider.BeginLifetimeScope();
		var handler = new HttpModelHandler(scope.Resolver, context);

		await handler.ProcessAsync<FooModel>();

		var model = handler.GetModel<FooModel>();

		// Assert

		Assert.That(handler.Processed, Is.True);
		Assert.That(model, Is.Not.Null);
		Assert.That(model.ID, Is.EqualTo(1));
		Assert.That(model.Name, Is.EqualTo("Bar"));

		validator.Verify(x => x.Validate(It.Is<FooModel>(m => m == originalModel), It.IsAny<IDIResolver>()));
	}

	[Test]
	public void ProcessAsync_GetModelCalledWithoutProcess_InvalidOperationException()
	{
		// Arrange
		var handler = new HttpModelHandler(null!, null!);

		// Act & Assert
		Assert.Throws<InvalidOperationException>(() => handler.GetModel<FooModel>());
	}
}