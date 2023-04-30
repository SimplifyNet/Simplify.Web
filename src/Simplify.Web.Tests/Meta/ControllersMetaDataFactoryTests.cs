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
		var roles = metaData.Security.RequiredUserRoles.ToList();

		// Assert

		Assert.AreEqual("TestController1", metaData.ControllerType.Name);
		Assert.AreEqual("/testaction", metaData.ExecParameters.RouteInfo.GetRoute);
		Assert.AreEqual("/testaction1", metaData.ExecParameters.RouteInfo.PostRoute);
		Assert.AreEqual("/testaction2", metaData.ExecParameters.RouteInfo.PutRoute);
		Assert.AreEqual("/testaction3", metaData.ExecParameters.RouteInfo.PatchRoute);
		Assert.AreEqual("/testaction4", metaData.ExecParameters.RouteInfo.DeleteRoute);
		Assert.IsTrue(metaData.Role.Is400Handler);
		Assert.IsTrue(metaData.Role.Is403Handler);
		Assert.IsTrue(metaData.Role.Is404Handler);
		Assert.AreEqual(1, metaData.ExecParameters.RunPriority);
		Assert.IsTrue(metaData.Security.IsAuthorizationRequired);
		Assert.AreEqual(2, roles.Count);
		Assert.AreEqual("Admin", roles[0]);
		Assert.AreEqual("User", roles[1]);
	}
}