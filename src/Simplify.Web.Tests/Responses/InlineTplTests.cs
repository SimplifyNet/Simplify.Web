#nullable disable

using System;
using Moq;
using NUnit.Framework;
using Simplify.Templates;
using Simplify.Web.Modules.Data;
using Simplify.Web.Responses;

namespace Simplify.Web.Tests.Responses
{
	[TestFixture]
	public class InlineTplTests
	{
		private Mock<IDataCollector> _dataCollector;

		[SetUp]
		public void Initialize()
		{
			_dataCollector = new Mock<IDataCollector>();
		}

		[Test]
		public void Process_DataCollectorVariableNameIsNullOrEmpty_ArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new InlineTpl(null, "foo"));
			Assert.Throws<ArgumentNullException>(() => new InlineTpl(null, TemplateBuilder.FromString("").Build()));
		}

		[Test]
		public void InlineTplProcess_NormalData_DataAddedtoDataCollector()
		{
			// Assign

			var tplData = new Mock<InlineTpl>("foo", "test") { CallBase = true };
			tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);

			// Act
			var result = tplData.Object.Process();

			// Assert

			_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "foo"), It.Is<string>(d => d == "test")));
			Assert.AreEqual(ControllerResponseResult.Default, result);
		}

		[Test]
		public void InlineTplProcess_NormalTemplate_DataAddedtoDataCollector()
		{
			// Assign

			var tplData = new Mock<InlineTpl>("foo", TemplateBuilder.FromString("test").Build()) { CallBase = true };
			tplData.SetupGet(x => x.DataCollector).Returns(_dataCollector.Object);

			// Act
			tplData.Object.Process();

			// Assert
			_dataCollector.Verify(x => x.Add(It.Is<string>(d => d == "foo"), It.Is<string>(d => d == "test")));
		}
	}
}