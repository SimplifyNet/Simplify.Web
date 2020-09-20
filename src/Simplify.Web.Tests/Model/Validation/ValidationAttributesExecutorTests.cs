#nullable disable

using NUnit.Framework;
using Simplify.Web.Model.Validation;
using Simplify.Web.Tests.Model.Validation.Attributes;
using Simplify.Web.Tests.TestEntities;
using Simplify.Web.Tests.TestEntities.HierarchyValidation;

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

		[Test]
		public void Validate_HierarchicalModel_NestedNullAttributeException()
		{
			// Arrange
			var model = new RootModel
			{
				//BuiltInType = "test",
				CustomType = new ChildModel
				{
					//BuiltInType = "test",
					CustomType = new SubChildModel()
				}
			};

			// Act

			var ex = Assert.Throws<ModelValidationException>(() => _validator.Validate(model, null));

			// Assert
			Assert.That(ex.Message,
				Does.StartWith($"Required property '{nameof(TestEntityWithProperty.Prop1)}' is null or empty"));
		}
	}
}