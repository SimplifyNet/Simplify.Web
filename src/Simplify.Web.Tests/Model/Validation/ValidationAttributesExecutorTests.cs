using NUnit.Framework;
using Simplify.Web.Model.Validation;

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
	}
}