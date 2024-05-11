using Moq;
using NUnit.Framework;
using Simplify.Web.Modules.Data;
using Simplify.Web.Page.Composition;
using Simplify.Web.Page.Generation;

namespace Simplify.Web.Tests.Page.Composition;

[TestFixture]
public class PageComposerTests
{
	private PageComposer _pageComposer = null!;

	private Mock<IDataCollector> _dataCollector = null!;
	private Mock<IPageGenerator> _pageGenerator = null!;

	private Mock<IPageCompositionStage> _stage1 = null!;
	private Mock<IPageCompositionStage> _stage2 = null!;

	[SetUp]
	public void Initialize()
	{
		_dataCollector = new Mock<IDataCollector>();
		_pageGenerator = new Mock<IPageGenerator>();
		_stage1 = new Mock<IPageCompositionStage>();
		_stage2 = new Mock<IPageCompositionStage>();

		_pageComposer = new PageComposer([_stage1.Object, _stage2.Object], _dataCollector.Object, _pageGenerator.Object);
	}

	[Test]
	public void Compose_StagesExecutedAndPageReturned()
	{
		// Arrange

		_pageGenerator.Setup(x => x.Generate(It.Is<IDataCollector>(dc => dc == _dataCollector.Object))).Returns("Foo");

		// Act
		var result = _pageComposer.Compose();

		// Assert

		Assert.That(result, Is.EqualTo("Foo"));

		_stage1.Verify(x => x.Execute(It.Is<IDataCollector>(dc => dc == _dataCollector.Object)));
		_stage2.Verify(x => x.Execute(It.Is<IDataCollector>(dc => dc == _dataCollector.Object)));
	}
}