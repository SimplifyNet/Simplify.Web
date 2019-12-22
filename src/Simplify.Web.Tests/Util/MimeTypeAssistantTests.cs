using System;
using NUnit.Framework;
using Simplify.Web.Util;

namespace Simplify.Web.Tests.Util
{
	[TestFixture]
	public class MimeTypeAssistantTests
	{
		[Test]
		public void GetMimeType_SomeExistingTypeWithDot_MimeTypeReturned()
		{
			// Arrange
			const string extension = ".txt";

			// Act
			var result = MimeTypeAssistant.GetMimeType(extension);

			// Assert
			Assert.AreEqual("text/plain", result);
		}

		[Test]
		public void GetMimeType_SomeExistingTypeWithoutDot_MimeTypeReturned()
		{
			// Arrange
			const string extension = "txt";

			// Act
			var result = MimeTypeAssistant.GetMimeType(extension);

			// Assert
			Assert.AreEqual("text/plain", result);
		}

		[Test]
		public void GetMimeTypeByFilePath_SomeFilePath_MimeTypeReturned()
		{
			// Arrange
			const string filePath = "C:\\MyFile.txt";

			// Act
			var result = MimeTypeAssistant.GetMimeTypeByFilePath(filePath);

			// Assert
			Assert.AreEqual("text/plain", result);
		}

		[Test]
		public void GetExtension_SomeExistingMimeType_FileExtensionReturned()
		{
			// Arrange
			const string mimeType = "text/plain";

			// Act
			var result = MimeTypeAssistant.GetExtension(mimeType);

			// Assert
			Assert.AreEqual(".txt", result);
		}

		[Test]
		public void GetExtension_NonExistingMimeType_FileExtensionReturned()
		{
			// Arrange
			const string mimeType = "test";

			// Act & Assert
			var result = Assert.Throws<ArgumentException>(() => MimeTypeAssistant.GetExtension(mimeType));

			// Assert
			Assert.AreEqual("Requested mime type is not registered: test", result.Message);
		}

		[Test]
		public void GetExtension_NonExistingMimeTypeNoExceptions_EmptyString()
		{
			// Arrange
			const string mimeType = "test";

			// Act & Assert
			var result = MimeTypeAssistant.GetExtension(mimeType, false);

			// Assert
			Assert.AreEqual("", result);
		}
	}
}