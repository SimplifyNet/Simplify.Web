using System;
using System.Linq;
using NUnit.Framework;
using Simplify.Web.Meta;
using Simplify.Web.Tests.TestEntities;

namespace Simplify.Web.Tests.Meta;

[TestFixture]
public class ControllersMetaDataFactoryTests
{
	[Test]
	public void CreateControllerMetaData_TestController_PropertiesSetCorrectly()
	{
		// Arrange
		var factory = new ControllerMetaDataFactory();

		// Act

		var metaData = factory.CreateControllerMetaData(typeof(TestController1));

		if (metaData.Security == null || metaData.ExecParameters == null || metaData.Role == null)
			throw new InvalidOperationException();

		var roles = metaData.Security.RequiredUserRoles!.ToList();

		// Assert

		Assert.AreEqual("TestController1", metaData.ControllerType.Name);
		Assert.AreEqual("/testaction", metaData.ExecParameters.Routes.First(x => x.Key == HttpMethod.Get).Value);
		Assert.AreEqual("/testaction1", metaData.ExecParameters.Routes.First(x => x.Key == HttpMethod.Post).Value);
		Assert.AreEqual("/testaction2", metaData.ExecParameters.Routes.First(x => x.Key == HttpMethod.Put).Value);
		Assert.AreEqual("/testaction3", metaData.ExecParameters.Routes.First(x => x.Key == HttpMethod.Patch).Value);
		Assert.AreEqual("/testaction4", metaData.ExecParameters.Routes.First(x => x.Key == HttpMethod.Delete).Value);
		Assert.AreEqual("/testaction5", metaData.ExecParameters.Routes.First(x => x.Key == HttpMethod.Options).Value);
		Assert.IsTrue(metaData.Role.Is400Handler);
		Assert.IsTrue(metaData.Role.Is403Handler);
		Assert.IsTrue(metaData.Role.Is404Handler);
		Assert.AreEqual(1, metaData.ExecParameters.RunPriority);
		Assert.IsTrue(metaData.Security.IsAuthorizationRequired);
		Assert.AreEqual(2, roles.Count);
		Assert.AreEqual("Admin", roles[0]);
		Assert.AreEqual("User", roles[1]);
	}

	[Test]
	public void CreateControllerMetaData_TestControllerV2_PropertiesSetCorrectly()
	{
		// Arrange
		var factory = new ControllerMetaDataFactory();

		// Act

		var metaData = factory.CreateControllerMetaData(typeof(TestControllerV2));

		// Assert

		Assert.That(metaData.ExecParameters, Is.Not.Null);
		Assert.That(metaData.ControllerType.Name, Is.EqualTo("TestControllerV2"));
		Assert.That(metaData.ExecParameters!.Routes.First(x => x.Key == HttpMethod.Get).Value, Is.EqualTo("/testaction"));
	}
}