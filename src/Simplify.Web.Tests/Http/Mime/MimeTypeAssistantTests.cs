using System;
using NUnit.Framework;
using Simplify.Web.Http.Mime;

namespace Simplify.Web.Tests.Http.Mime;

[TestFixture]
public class MimeTypeAssistantTests
{
	[Test]
	public void GetExtension_ArgumentNull_ArgumentException()
	{
		// Act & Assert
		var result = Assert.Throws<ArgumentException>(() => MimeTypeAssistant.GetExtension(null!));

		// Assert
		Assert.That(result?.Message, Does.Contain("Value cannot be null or empty."));
	}

	[Test]
	public void GetExtension_NonExistingMimeType_ArgumentException()
	{
		// Arrange
		const string mimeType = "test";

		// Act & Assert
		var result = Assert.Throws<ArgumentException>(() => MimeTypeAssistant.GetExtension(mimeType));

		// Assert
		Assert.That(result?.Message, Is.EqualTo("Requested mime type is not registered: test"));
	}

	[Test]
	public void GetExtension_StartsWithDot_ArgumentException()
	{
		// Act & Assert
		var result = Assert.Throws<ArgumentException>(() => MimeTypeAssistant.GetExtension(".foo"));

		// Assert
		Assert.That(result?.Message, Is.EqualTo("Requested mime type is not valid: .foo"));
	}

	[Test]
	public void GetExtension_NonExistingMimeTypeNoExceptions_EmptyString()
	{
		// Arrange
		const string mimeType = "test";

		// Act & Assert
		var result = MimeTypeAssistant.GetExtension(mimeType, false);

		// Assert
		Assert.That(result, Is.EqualTo(""));
	}

	[Test]
	public void GetExtension_SomeExistingMimeType_FileExtensionReturned()
	{
		// Arrange
		const string mimeType = "text/plain";

		// Act
		var result = MimeTypeAssistant.GetExtension(mimeType);

		// Assert
		Assert.That(result, Is.EqualTo(".txt"));
	}

	[Test]
	public void GetMimeType_SomeExistingTypeWithDot_MimeTypeReturned()
	{
		// Arrange
		const string extension = ".txt";

		// Act
		var result = MimeTypeAssistant.GetMimeType(extension);

		// Assert
		Assert.That(result, Is.EqualTo("text/plain"));
	}

	[Test]
	public void GetMimeType_SomeExistingTypeWithoutDot_MimeTypeReturned()
	{
		// Arrange
		const string extension = "txt";

		// Act
		var result = MimeTypeAssistant.GetMimeType(extension);

		// Assert
		Assert.That(result, Is.EqualTo("text/plain"));
	}

	[Test]
	public void GetMimeTypeByFilePath_SomeFilePath_MimeTypeReturned()
	{
		// Arrange
		const string filePath = "C:\\MyFile.txt";

		// Act
		var result = MimeTypeAssistant.GetMimeTypeByFilePath(filePath);

		// Assert
		Assert.That(result, Is.EqualTo("text/plain"));
	}

	[Test]
	public void GetMimeType_UndefinedMimeType_DefaultMimeTypeReturned()
	{
		// Arrange
		const string extension = ".foo";

		// Act
		var result = MimeTypeAssistant.GetMimeType(extension);

		// Assert
		Assert.That(result, Is.EqualTo("application/octet-stream"));
	}
}