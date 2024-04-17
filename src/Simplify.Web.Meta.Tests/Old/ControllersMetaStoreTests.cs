using System;
using Moq;
using NUnit.Framework;
using Simplify.Web.Meta.Tests.Old.TestTypes;
using Simplify.Web.Old.Attributes.Setup;
using Simplify.Web.Old.Meta;

namespace Simplify.Web.Meta.Tests.Old;

[TestFixture]
[IgnoreControllers(typeof(TestController3))]
public class ControllersMetaStoreTests
{
	[Test]
	public void GetControllersMetaData_LocalControllers_GetWithoutIgnored()
	{
		// Arrange

		SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Remove("Simplify");
		SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Add("DynamicProxyGenAssembly2");
		SimplifyWebTypesFinder.CleanLoadedTypesAndAssembliesInfo();

		var factory = new Mock<IControllerMetaDataFactory>();
		var store = new ControllersMetaStore(factory.Object);

		factory.SetupSequence(x => x.CreateControllerMetaData(It.IsAny<Type>()))
			.Returns(Mock.Of<IControllerMetaData>(x => x.ControllerType == typeof(TestControllerV2)))
			.Returns(Mock.Of<IControllerMetaData>(x => x.ControllerType == typeof(TestControllerV2WithModel)))
			.Returns(Mock.Of<IControllerMetaData>(x => x.ControllerType == typeof(TestController1) && x.ExecParameters == new
				Web.Old.Meta.ControllerExecParameters(null, 2)))
			.Returns(Mock.Of<IControllerMetaData>(x => x.ControllerType == typeof(TestController6)))
			.Returns(Mock.Of<IControllerMetaData>(x => x.ControllerType == typeof(TestController2) && x.ExecParameters == new
				Web.Old.Meta.ControllerExecParameters(null, 1)))
			.Returns(Mock.Of<IControllerMetaData>(x => x.ControllerType == typeof(TestController4)))
			.Returns(Mock.Of<IControllerMetaData>(x => x.ControllerType == typeof(TestController5)));

		// Act
		var metaData = store.ControllersMetaData;

		// Assert

		factory.Verify(x => x.CreateControllerMetaData(It.Is<Type>(t => t == typeof(TestControllerV2))));
		factory.Verify(x => x.CreateControllerMetaData(It.Is<Type>(t => t == typeof(TestControllerV2WithModel))));
		factory.Verify(x => x.CreateControllerMetaData(It.Is<Type>(t => t == typeof(TestController1))));
		factory.Verify(x => x.CreateControllerMetaData(It.Is<Type>(t => t == typeof(TestController2))));
		factory.Verify(x => x.CreateControllerMetaData(It.Is<Type>(t => t == typeof(TestController4))));
		factory.Verify(x => x.CreateControllerMetaData(It.Is<Type>(t => t == typeof(TestController5))));
		factory.Verify(x => x.CreateControllerMetaData(It.Is<Type>(t => t == typeof(TestController6))));

		Assert.AreEqual(7, metaData.Count);
	}
}