using System.Linq;
using NUnit.Framework;
using Simplify.Web.Controllers.V1.Metadata;
using Simplify.Web.Http;
using Simplify.Web.Meta.Tests.TestTypes.Controllers.V1;

namespace Simplify.Web.Meta.Tests.Controllers.V1.Metadata;

[TestFixture]
public class Controller1MetadataFactoryTests
{
	[Test]
	public void Create_AllAttributesController_PropertiesSetCorrectly()
	{
		// Arrange
		var factory = new Controller1MetadataFactory();

		// Act
		var metaData = factory.Create(typeof(AllAttributesController));

		// Assert

		Assert.That(metaData.ControllerType, Is.EqualTo(typeof(AllAttributesController)));
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

		Assert.That(metaData.ExecParameters!.Routes.First(x => x.Key == HttpMethod.Get).Value, Is.EqualTo("/test-action"));
		Assert.That(metaData.ExecParameters!.Routes.First(x => x.Key == HttpMethod.Post).Value, Is.EqualTo("/test-action1"));
		Assert.That(metaData.ExecParameters!.Routes.First(x => x.Key == HttpMethod.Put).Value, Is.EqualTo("/test-action2"));
		Assert.That(metaData.ExecParameters!.Routes.First(x => x.Key == HttpMethod.Patch).Value, Is.EqualTo("/test-action3"));
		Assert.That(metaData.ExecParameters!.Routes.First(x => x.Key == HttpMethod.Delete).Value, Is.EqualTo("/test-action4"));
		Assert.That(metaData.ExecParameters!.Routes.First(x => x.Key == HttpMethod.Options).Value, Is.EqualTo("/test-action5"));
	}
}