﻿using Moq;
using NUnit.Framework;
using SampleApp.Classic.Controllers;
using Simplify.Web.Responses;

namespace SampleApp.Classic.Tests.Controllers;

[TestFixture]
public class DefaultPageControllerTests
{
	[Test]
	public void Invoke_Default_MainContentSet()
	{
		// Arrange
		var c = new Mock<DefaultController> { CallBase = true };

		// Act
		var result = c.Object.Invoke();

		// Assert
		Assert.That(((StaticTpl)result).TemplateFileName, Is.EqualTo("Default"));
	}
}