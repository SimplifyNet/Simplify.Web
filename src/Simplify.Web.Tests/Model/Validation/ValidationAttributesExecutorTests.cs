#nullable disable

using NUnit.Framework;
using Simplify.Web.Model.Validation;
using Simplify.Web.Tests.Model.Validation.Attributes;
using Simplify.Web.Tests.TestEntities;

namespace Simplify.Web.Tests.Model.Validation
{
	[TestFixture]
	public class ValidationAttributesExecutorTests
	{
		private ValidationAttributesExecutor _validator;

		[SetUp]
		public void Initialize()
		{
			_validator = new ValidationAttributesExecutor();
		}

		[Test]
		public void Validate_ModelWithOnePropertyAndOneValidateAttribute_AttributeValidationCalled()
		{
			// Assign
			var model = new TestModel();

			// Act

			var ex = Assert.Throws<ModelValidationException>(() => _validator.Validate(model, null));

			// Assert
			Assert.That(ex.Message,
				Does.StartWith($"Required property '{nameof(TestEntityWithProperty.Prop1)}' is null or empty"));
		}
	}
}