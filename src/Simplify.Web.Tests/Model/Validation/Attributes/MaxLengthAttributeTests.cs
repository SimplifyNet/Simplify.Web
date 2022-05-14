using NUnit.Framework;
using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Model.Validation.Attributes;

[TestFixture]
public class MaxLengthAttributeTests : AttributesTestBase
{
	public const int MaximumPropertyLength = 2;

	[OneTimeSetUp]
	public void SetupAttribute() => Attr = new MaxLengthAttribute(MaximumPropertyLength);

	[Test]
	public void Validate_MinLengthOk_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue("a");
	}

	[Test]
	public void Validate_MinLengthNull_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue(null);
	}

	[Test]
	public void Validate_AboveMaxLength_ExceptionThrown()
	{
		// Assign

		const string value = "test";
		var defaultMessage = $"Property '{nameof(TestEntityWithProperty.Prop1)}' required maximum length is '{MaximumPropertyLength}', actual value: '{value}'";

		// Act & Assert
		TestAttribute(value, defaultMessage);
	}
}