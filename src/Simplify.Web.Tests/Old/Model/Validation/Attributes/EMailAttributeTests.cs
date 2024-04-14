using NUnit.Framework;
using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Old.Model.Validation.Attributes;

[TestFixture]
public class EMailAttributeTests : AttributesTestBase
{
	[OneTimeSetUp]
	public void SetupAttribute() => Attr = new EMailAttribute();

	[Test]
	public void Validate_ValidEMail_NoExceptions()
	{
		// Act & Assert
		TestAttributeForValidValue("test@test.test");
	}

	[Test]
	public void Validate_NullEMail_NoExceptions()
	{
		// Act & Assert
		TestAttributeForValidValue(null);
	}

	[Test]
	public void Validate_InvalidEMail_NoExceptions()
	{
		// Assign

		const string value = "test";
		var defaultMessage = $"Property '{nameof(TestEntityWithProperty.Prop1)}' should be an email, actual value: '{value}'";

		// Act & Assert
		TestAttribute(value, defaultMessage);
	}
}