using System;
using Moq;
using NUnit.Framework;
using Simplify.Web.Attributes.Setup;
using Simplify.Web.Meta;
using Simplify.Web.Tests.TestEntities;

namespace Simplify.Web.Tests.Meta
{
	[TestFixture]
	[IgnoreControllers(typeof(TestController3))]
	public class ControllersMetaStoreTests
	{
		[Test]
		public void GetControllersMetaData_LocalControllers_GetWithoutIgnoredAndSortedCorrectly()
		{
			// Assign

			SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Remove("Simplify");
			SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Add("DynamicProxyGenAssembly2");
			SimplifyWebTypesFinder.CleanLoadedTypesAndAssembliesInfo();

			var factory = new Mock<IControllerMetaDataFactory>();
			var store = new ControllersMetaStore(factory.Object);

			factory.SetupSequence(x => x.CreateControllerMetaData(It.IsAny<Type>()))
				.Returns(new ControllerMetaData(typeof(TestController1), new ControllerExecParameters(null, 2)))
				.Returns(new ControllerMetaData(typeof(TestController2), new ControllerExecParameters(null, 1)))
				.Returns(new ControllerMetaData(typeof(TestController4)))
				.Returns(new ControllerMetaData(typeof(TestController5)));

			// Act
			var metaData = store.ControllersMetaData;

			Assert.AreEqual(4, metaData.Count);
			Assert.AreEqual("TestController4", metaData[0].ControllerType.Name);
			Assert.AreEqual("TestController5", metaData[1].ControllerType.Name);
			Assert.AreEqual("TestController2", metaData[2].ControllerType.Name);
			Assert.AreEqual("TestController1", metaData[3].ControllerType.Name);

			factory.Verify(x => x.CreateControllerMetaData(It.Is<Type>(t => t == typeof(TestController1))));
			factory.Verify(x => x.CreateControllerMetaData(It.Is<Type>(t => t == typeof(TestController2))));
			factory.Verify(x => x.CreateControllerMetaData(It.Is<Type>(t => t == typeof(TestController4))));
			factory.Verify(x => x.CreateControllerMetaData(It.Is<Type>(t => t == typeof(TestController5))));
		}
	}
}