using NUnit.Framework;
using Simplify.Web.Model.Validation;
using Simplify.Web.Tests.TestEntities;

namespace Simplify.Web.Tests.Model.Validation
{
	[TestFixture]
	public class StringValidatorTests
	{
		[Test]
		public void Validate_RegexOk_Ok()
		{
			StringValidator.Validate("test", typeof(TestModelRegex).GetProperties()[0]);
		}

		[Test]
		public void Validate_InvalidRegex_ExceptionThrown()
		{
			Assert.Throws<ModelValidationException>(() => StringValidator.Validate("test1", typeof(TestModelRegex).GetProperties()[0]));
		}

		[Test]
		public void Validate_RegexNull_ExceptionThrown()
		{
			Assert.Throws<ModelValidationException>(() => StringValidator.Validate(null, typeof(TestModelRegex).GetProperties()[0]));
		}
	}
}