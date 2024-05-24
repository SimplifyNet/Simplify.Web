using System.Linq;
using NUnit.Framework;
using Simplify.Web.Controllers.V2.Metadata;
using Simplify.Web.Http;
using Simplify.Web.Tests.Controllers.V2.Metadata.MetadataFactoryTests.TestTypes;

namespace Simplify.Web.Tests.Controllers.V2.Metadata.MetadataFactoryTests;

[TestFixture]
public class Controller2MetadataFactoryTests
{
	[Test]
	public void Create_AllAttributesController_PropertiesSetCorrectly()
	{
		// Arrange
		var factory = new Controller2MetadataFactory();

		// Act
		var metaData = factory.Create(typeof(AllAttributesControllerV2));

		// Assert

		Assert.That(metaData.ControllerType, Is.EqualTo(typeof(AllAttributesControllerV2)));
		Assert.That(metaData.Role, Is.Not.Null);
		Assert.That(metaData.Role!.IsForbiddenHandler, Is.True);
		Assert.That(metaData.Role!.IsNotFoundHandler, Is.True);
		Assert.That(metaData.Security, Is.Not.Null);
		Assert.That(metaData.Security!.IsAuthorizationRequired, Is.True);
		Assert.That(metaData.ExecParameters, Is.Not.Null);
		Assert.That(metaData.ExecParameters!.RunPriority, Is.EqualTo(1));

		var roles = metaData.Security!.RequiredUserRoles!.ToList();

		Assert.That(roles.Count, Is.EqualTo(2));
		Assert.That(roles[0], Is.EqualTo("Admin"));
		Assert.That(roles[1], Is.EqualTo("User"));

		Assert.That(metaData.ExecParameters.Routes.Count, Is.EqualTo(6));

		var firstControllerRoute = metaData.ExecParameters.Routes.First(x => x.Key == HttpMethod.Get);

		Assert.That(firstControllerRoute.Value.Items.Count, Is.EqualTo(1));
		Assert.That(firstControllerRoute.Value.Path, Is.EqualTo("/test-action"));
		Assert.That(firstControllerRoute.Value.Items[0].Name, Is.EqualTo("test-action"));

		Assert.That(metaData.ExecParameters.Routes.First(x => x.Key == HttpMethod.Post).Value.Path, Is.EqualTo("/test-action1"));
		Assert.That(metaData.ExecParameters.Routes.First(x => x.Key == HttpMethod.Put).Value.Path, Is.EqualTo("/test-action2"));
		Assert.That(metaData.ExecParameters.Routes.First(x => x.Key == HttpMethod.Patch).Value.Path, Is.EqualTo("/test-action3"));
		Assert.That(metaData.ExecParameters.Routes.First(x => x.Key == HttpMethod.Delete).Value.Path, Is.EqualTo("/test-action4"));
		Assert.That(metaData.ExecParameters.Routes.First(x => x.Key == HttpMethod.Options).Value.Path, Is.EqualTo("/test-action5"));
	}

	[Test]
	public void Create_AllParamsController_ParamsParsedCorrectly()
	{
		// Arrange
		var factory = new Controller2MetadataFactory();

		// Act
		var metaData = (IController2Metadata)factory.Create(typeof(AllParamsControllerV2));

		// Assert

		Assert.That(metaData.InvokeMethodParameters["stringparam"], Is.EqualTo(typeof(string)));
	}
}