using NUnit.Framework;
using Simplify.Web.Model.Validation.Attributes;

namespace Simplify.Web.Tests.Model.Validation.Attributes;

[TestFixture]
public class RegexAttributeTests : AttributesTestBase
{
	private const string RegexPattern = "^[a-zA-Z]+$";

	[OneTimeSetUp]
	public void SetupAttribute() => Attr = new RegexAttribute(RegexPattern);

	[Test]
	public void Validate_RegexOk_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue("test");
	}

	[Test]
	public void Validate_RegexNull_Ok()
	{
		// Act & Assert
		TestAttributeForValidValue(null);
	}

	[Test]
	public void Validate_InvalidRegex_ExceptionThrown()
	{
		// Assign

		const string value = "test1";
		var defaultMessage = $"Property '{nameof(TestEntityWithProperty.Prop1)}' regex not matched, actual value: '{value}', pattern: '{RegexPattern}'";

		// Act & Assert
		TestAttribute(value, defaultMessage);
	}
}