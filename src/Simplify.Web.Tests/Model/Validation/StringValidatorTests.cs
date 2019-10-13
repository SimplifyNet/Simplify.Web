using NUnit.Framework;
using Simplify.Web.Model.Validation;
using Simplify.Web.Tests.TestEntities;

namespace Simplify.Web.Tests.Model.Validation
{
	[TestFixture]
	public class StringValidatorTests
	{
		[Test]
		public void Validate_MinLengthOk_Ok()
		{
			StringValidator.Validate("test", typeof(TestModelMinLength).GetProperties()[0]);
		}

		[Test]
		public void Validate_BelowMinLength_ExceptionThrown()
		{
			Assert.Throws<ModelValidationException>(() => StringValidator.Validate("a", typeof(TestModelMinLength).GetProperties()[0]));
		}

		[Test]
		public void Validate_MinLengthNull_ExceptionThrown()
		{
			Assert.Throws<ModelValidationException>(() => StringValidator.Validate(null, typeof(TestModelMinLength).GetProperties()[0]));
		}

		[Test]
		public void Validate_MaxLengthOk_Ok()
		{
			StringValidator.Validate("a", typeof(TestModelMaxLength).GetProperties()[0]);
		}

		[Test]
		public void Validate_AboveMaxLength_ExceptionThrown()
		{
			Assert.Throws<ModelValidationException>(() => StringValidator.Validate("test", typeof(TestModelMaxLength).GetProperties()[0]));
		}

		[Test]
		public void Validate_MaxLengthNull_ExceptionThrown()
		{
			Assert.Throws<ModelValidationException>(() => StringValidator.Validate(null, typeof(TestModelMaxLength).GetProperties()[0]));
		}

		[Test]
		public void Validate_EmailOk_Ok()
		{
			StringValidator.Validate("test@test.test", typeof(TestModelEMail).GetProperties()[0]);
		}

		[Test]
		public void Validate_InvalidEMail_ExceptionThrown()
		{
			Assert.Throws<ModelValidationException>(() => StringValidator.Validate("test", typeof(TestModelEMail).GetProperties()[0]));
		}

		[Test]
		public void Validate_NullEMail_ExceptionThrown()
		{
			Assert.Throws<ModelValidationException>(() => StringValidator.Validate(null, typeof(TestModelEMail).GetProperties()[0]));
		}

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