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

#if NET8_0_OR_GREATER

[TestFixture]
public class HttpQueryModelBinderTests
{
	[Test]
	public async Task BindAsync_NotMatchedRequestMethod_NotBoundAndModelIsNull()
	{
		// Arrange

		var context = Mock.Of<IWebContext>(x => x.Request.Method == "POST");
		var args = new ModelBinderEventArgs<FooModel>(context);

		// Act
		await new HttpQueryModelBinder().BindAsync(args);

		// Assert

		Assert.That(args.IsBound, Is.False);
		Assert.That(args.Model, Is.Null);
	}

	[Test]
	public async Task BindAsync_MatchedRequestMethodAndDataExist_Bound()
	{
		// Arrange

		var query = new Dictionary<string, StringValues>
		{
			{ "ID", "1" },
			{ "Name", "Bar" }
		};

		var context = Mock.Of<IWebContext>(x =>
			x.Request.Method == "GET" &&
			x.Query == new QueryCollection(query));

		var args = new ModelBinderEventArgs<FooModel>(context);

		// Act
		await new HttpQueryModelBinder().BindAsync(args);

		// Assert

		Assert.That(args.IsBound, Is.True);
		Assert.That(args.Model, Is.Not.Null);
		Assert.That(args.Model.ID, Is.EqualTo(1));
		Assert.That(args.Model.Name, Is.EqualTo("Bar"));
	}

	[Test]
	public async Task BindAsync_MatchedRequestMethodAndNoDataExist_BoundWithDefaultValues()
	{
		// Arrange

		var context = Mock.Of<IWebContext>(x =>
			x.Request.Method == "GET" &&
			x.Query == new QueryCollection(new Dictionary<string, StringValues>()));

		var args = new ModelBinderEventArgs<FooModel>(context);

		// Act
		await new HttpQueryModelBinder().BindAsync(args);

		// Assert

		Assert.That(args.IsBound, Is.True);
		Assert.That(args.Model, Is.Not.Null);
		Assert.That(args.Model.ID, Is.EqualTo(0));
		Assert.That(args.Model.Name, Is.EqualTo(null));
	}
}

#endif