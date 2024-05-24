using System.Linq;
using NUnit.Framework;
using Simplify.Web.Controllers.Meta;
using Simplify.Web.Controllers.V1.Metadata;
using Simplify.Web.Http;
using Simplify.Web.Tests.Controllers.V1.Metadata.MetadataFactoryTests.TestTypes;

namespace Simplify.Web.Tests.Controllers.V1.Metadata.MetadataFactoryTests;

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

		var roles = metaData.Security!.RequiredUserRoles!.ToList();

		Assert.That(roles.Count, Is.EqualTo(2));
		Assert.That(roles[0], Is.EqualTo("Admin"));
		Assert.That(roles[1], Is.EqualTo("User"));

		AssertExecParameters(metaData);
	}

	private static void AssertExecParameters(IControllerMetadata metaData)
	{
		Assert.That(metaData.ExecParameters, Is.Not.Null);
		Assert.That(metaData.ExecParameters!.RunPriority, Is.EqualTo(1));

		Assert.That(metaData.ExecParameters.Routes.Count, Is.EqualTo(6));

		var firstControllerRoute = metaData.ExecParameters.Routes.First(x => x.Key == HttpMethod.Get);

		Assert.That(firstControllerRoute.Value.Items.Count, Is.EqualTo(1));
		Assert.That(firstControllerRoute.Value.Path, Is.EqualTo("/test-action"));
		Assert.That(firstControllerRoute.Value.Items[0].Name, Is.EqualTo("test-action"));

		AssertExecParametersRoutes(metaData.ExecParameters);
	}

	private static void AssertExecParametersRoutes(ControllerExecParameters execParameters)
	{
		Assert.That(execParameters.Routes[HttpMethod.Post].Path, Is.EqualTo("/test-action1"));
		Assert.That(execParameters.Routes[HttpMethod.Put].Path, Is.EqualTo("/test-action2"));
		Assert.That(execParameters.Routes[HttpMethod.Patch].Path, Is.EqualTo("/test-action3"));
		Assert.That(execParameters.Routes[HttpMethod.Delete].Path, Is.EqualTo("/test-action4"));
		Assert.That(execParameters.Routes[HttpMethod.Options].Path, Is.EqualTo("/test-action5"));
	}
}