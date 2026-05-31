using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using Simplify.Web.StaticFiles.IO;

namespace Simplify.Web.Tests.StaticFiles.IO;

[TestFixture]
public class StaticFileTests
{
	private const string ValidRelativeFilePath = "StaticFiles/IO/TestFiles/TestStaticFile.html";

	// ReSharper disable once StringLiteralTypo
	private readonly IReadOnlyList<string> _validPaths = ["staticfiles"];

	private readonly string _sitePhysicalPath = Directory.GetCurrentDirectory() + "/";

	private StaticFile _staticFile = null!;

	[SetUp]
	public void Initialize()
	{
		_staticFile = new StaticFile(_validPaths, _sitePhysicalPath);
	}

	[Test]
	public void IsValidPath_AllowedPathAndValidFile_True()
	{
		// Act
		var result = _staticFile.IsValidPath(ValidRelativeFilePath);

		// Assert
		Assert.That(result, Is.True);
	}

	[Test]
	public void IsValidPath_ValidFileButPathIsNotAllowed_False()
	{
		// Arrange
		var staticFile = new StaticFile([], _sitePhysicalPath);

		// Act
		var result = staticFile.IsValidPath(ValidRelativeFilePath);

		// Assert
		Assert.That(result, Is.False);
	}

	[Test]
	public void IsValidPath_AllowedPathButInvalidFile_False()
	{
		// Arrange
		const string relativeFilePath = "StaticFiles/IO/TestFiles/NotExistent.html";

		// Act
		var result = _staticFile.IsValidPath(relativeFilePath);

		// Assert
		Assert.That(result, Is.False);
	}

	[Test]
	public void GetLastModificationTime_ValidFile_MillisecondsTrimmed()
	{
		// Act
		var result = _staticFile.GetLastModificationTime(ValidRelativeFilePath);

		// Assert

		Assert.That(result.Year, Is.Not.Zero);
		Assert.That(result.Millisecond, Is.Zero);
	}

	[Test]
	public void IsValidPath_ParentDirectoryTraversal_False()
	{
		// Act
		var result = _staticFile.IsValidPath("staticfiles/../StaticFiles/IO/TestFiles/TestStaticFile.html");

		// Assert
		Assert.That(result, Is.False);
	}

	[Test]
	public void IsValidPath_EncodedTraversalLooking_False()
	{
		// Act — ASP.NET Core decodes %2e%2e before this layer, so the raw '..' check fires.
		var result = _staticFile.IsValidPath("staticfiles/../../../etc/passwd");

		// Assert
		Assert.That(result, Is.False);
	}

	[Test]
	public void GetDataAsync_TraversalPath_Throws() =>
		Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
			await _staticFile.GetDataAsync("staticfiles/../StaticFiles/IO/TestFiles/TestStaticFile.html"));

	[Test]
	public async Task GetDataAsync_ValidFile_DataReturned()
	{
		// Act
		var result = await _staticFile.GetDataAsync(ValidRelativeFilePath);

		// Assert
		Assert.That(result, Is.EqualTo("Foo"u8.ToArray()));
	}
}