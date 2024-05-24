using System;
using NUnit.Framework;
using Simplify.Web.Controllers.V2.Metadata;
using Simplify.Web.Tests.Controllers.V2.Metadata.MetadataTests.TestTypes;

namespace Simplify.Web.Tests.Controllers.V2.Metadata.MetadataTests;

[TestFixture]
public class Controller2MetadataTests
{
	[TestCase(typeof(VoidResultController))]
	[TestCase(typeof(TaskResultController))]
	[TestCase(typeof(ControllerResponseResultController))]
	[TestCase(typeof(TaskControllerResponseResultController))]
	public void Ctor_ValidResult_NoException(Type type)
	{
		// Act
		var md = new Controller2Metadata(type);

		// Assert
		Assert.That(md, Is.Not.Null);
	}

	[TestCase(typeof(BadReturnTypeController))]
	[TestCase(typeof(EmptyController))]
	public void Ctor_InvalidController_InvalidOperationException(Type type)
	{
		// Act
		Assert.Throws<InvalidOperationException>(() => new Controller2Metadata(type));
	}
}