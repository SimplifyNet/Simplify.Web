using NUnit.Framework;
using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Old.Model.Validation.Attributes;

[TestFixture]
public class MinLengthAttributeTests : AttributesTestBase
{
	public const int MinimumPropertyLength = 2;

	[OneTimeSetUp]
	public void SetupAttribute() => Attr = new MinLengthAttribute(MinimumPropertyLength);

	[Test]
	public void Validate_MinLengthOk_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue("test");
	}

	[Test]
	public void Validate_MinLengthNull_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue(null);
	}

	[Test]
	public void Validate_BelowMinLength_ExceptionThrown()
	{
		// Assign

		const string value = "a";
		var defaultMessage = $"Property '{nameof(TestEntityWithProperty.Prop1)}' required minimum length is '{MinimumPropertyLength}', actual value: '{value}'";

		// Act & Assert
		TestAttribute(value, defaultMessage);
	}
}