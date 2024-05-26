using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;
using Simplify.Web.Model.Binding;
using Simplify.Web.Model.Binding.Binders;
using Simplify.Web.Modules.Context;
using Simplify.Web.Tests.Model.Binding.Binders.TestTypes;

namespace Simplify.Web.Tests.Model.Binding.Binders;

[TestFixture]
public class HttpFormModelBinderTests
{
	[TestCase(null)]
	[TestCase("foo")]
	public async Task BindAsync_NotMatchedContentType_NotBoundAndModelIsNull(string contentType)
	{
		// Arrange

		var context = Mock.Of<IWebContext>(x => x.Request.ContentType == contentType);
		var args = new ModelBinderEventArgs<FooModel>(context);

		// Act
		await new HttpFormModelBinder().BindAsync(args);

		// Assert

		Assert.That(args.IsBound, Is.False);
		Assert.That(args.Model, Is.Null);
	}

	[Test]
	public async Task BindAsync_MatchedContentTypeAndDataExist_Bound()
	{
		// Arrange

		var query = new Dictionary<string, StringValues>
		{
			{ "ID", "1" },
			{ "Name", "Bar" }
		};

		var context = new Mock<IWebContext>();

		context.SetupGet(x => x.Request.ContentType).Returns("application/x-www-form-urlencoded");
		context.SetupGet(x => x.Form).Returns(new FormCollection(query));

		var args = new ModelBinderEventArgs<FooModel>(context.Object);

		// Act
		await new HttpFormModelBinder().BindAsync(args);

		// Assert

		Assert.That(args.IsBound, Is.True);
		Assert.That(args.Model, Is.Not.Null);
		Assert.That(args.Model.ID, Is.EqualTo(1));
		Assert.That(args.Model.Name, Is.EqualTo("Bar"));

		context.Verify(x => x.ReadFormAsync());
	}

	[Test]
	public async Task BindAsync_MatchedContentTypeAndNoDataExist_BoundWithDefaultValues()
	{
		// Arrange

		var context = Mock.Of<IWebContext>(x =>
			x.Request.ContentType == "application/x-www-form-urlencoded" &&
			x.Form == new FormCollection(new Dictionary<string, StringValues>(), null));

		var args = new ModelBinderEventArgs<FooModel>(context);

		// Act
		await new HttpFormModelBinder().BindAsync(args);

		// Assert

		Assert.That(args.IsBound, Is.True);
		Assert.That(args.Model, Is.Not.Null);
		Assert.That(args.Model.ID, Is.EqualTo(0));
		Assert.That(args.Model.Name, Is.EqualTo(null));
	}
}