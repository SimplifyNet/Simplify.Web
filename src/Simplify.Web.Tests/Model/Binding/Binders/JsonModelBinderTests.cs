using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Simplify.Web.Model.Binding;
using Simplify.Web.Model.Binding.Binders;
using Simplify.Web.Model.Validation;
using Simplify.Web.Modules.Context;
using Simplify.Web.Tests.Model.Binding.Binders.TestTypes;

namespace Simplify.Web.Tests.Model.Binding.Binders;

[TestFixture]
public class JsonModelBinderTests
{
	[TestCase(null)]
	[TestCase("foo")]
	public async Task BindAsync_NotMatchedContentType_NotBoundAndModelIsNull(string contentType)
	{
		// Arrange

		var context = Mock.Of<IWebContext>(x => x.Request.ContentType == contentType);
		var args = new ModelBinderEventArgs<FooModel>(context);

		// Act
		await new JsonModelBinder().BindAsync(args);

		// Assert

		Assert.That(args.IsBound, Is.False);
		Assert.That(args.Model, Is.Null);
	}

	[Test]
	public async Task BindAsync_MatchedContentTypeAndDataExist_Bound()
	{
		// Arrange

		var content =
		"""
		{
			"ID": 1,
			"Name": "Bar"
		}
		""";

		var context = new Mock<IWebContext>();

		context.SetupGet(x => x.Request.ContentType).Returns("application/json");
		context.SetupGet(x => x.RequestBody).Returns(content);

		var args = new ModelBinderEventArgs<FooModel>(context.Object);

		// Act
		await new JsonModelBinder().BindAsync(args);

		// Assert

		Assert.That(args.IsBound, Is.True);
		Assert.That(args.Model, Is.Not.Null);
		Assert.That(args.Model.ID, Is.EqualTo(1));
		Assert.That(args.Model.Name, Is.EqualTo("Bar"));

		context.Verify(x => x.ReadRequestBodyAsync());
	}

	[Test]
	public void BindAsync_MatchedContentTypeAndCorruptedData_Exception()
	{
		// Arrange

		var content =
		"""
		{
			"ID": 1,
			"Name": "Bar"
		""";

		var context = new Mock<IWebContext>();

		context.SetupGet(x => x.Request.ContentType).Returns("application/json");
		context.SetupGet(x => x.RequestBody).Returns(content);

		var args = new ModelBinderEventArgs<FooModel>(context.Object);

		// Act & Assert
		Assert.ThrowsAsync<ModelValidationException>(async () => await new JsonModelBinder().BindAsync(args));
	}

	[TestCase(null)]
	[TestCase("")]
	public void BindAsync_MatchedContentTypeAndNoDataExist_ModelValidationException(string content)
	{
		// Arrange

		var context = Mock.Of<IWebContext>(x =>
			x.Request.ContentType == "application/json" &&
			x.RequestBody == content);

		var args = new ModelBinderEventArgs<FooModel>(context);

		// Act & Assert
		Assert.ThrowsAsync<ModelValidationException>(async () => await new JsonModelBinder().BindAsync(args));
	}
}