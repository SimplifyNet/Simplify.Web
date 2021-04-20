using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;
using Simplify.Web.Modules;
using Simplify.Web.Settings;

namespace Simplify.Web.Tests.Modules
{
	[TestFixture]
	public class LanguageManagerTests
	{
		private LanguageManager _languageManager = null!;
		private Mock<ISimplifyWebSettings> _settings = null!;
		private Mock<HttpContext> _context = null!;

		private Mock<IResponseCookies> _responseCookies = null!;

		[SetUp]
		public void Initialize()
		{
			_settings = new Mock<ISimplifyWebSettings>();
			_context = new Mock<HttpContext>();

			_settings.SetupGet(x => x.DefaultLanguage).Returns("en");
			_responseCookies = new Mock<IResponseCookies>();

			_context.SetupGet(x => x.Request.Cookies).Returns(Mock.Of<IRequestCookieCollection>());
			_context.SetupGet(x => x.Response.Cookies).Returns(_responseCookies.Object);

			_languageManager = new LanguageManager(_settings.Object, _context.Object);
		}

		[Test]
		public void Constructor_NoRequestCookieLanguage_DefaultLanguageSet()
		{
			// Assert
			Assert.AreEqual("en", _languageManager.Language);
		}

		[Test]
		public void Constructor_HaveRequestCookie_CurrentLanguageSet()
		{
			// Assign

			var cookieCollection = new Mock<IRequestCookieCollection>();
			cookieCollection.SetupGet(x => x[It.Is<string>(s => s == LanguageManager.CookieLanguageFieldName)]).Returns("ru");
			_context.SetupGet(x => x.Request.Cookies).Returns(cookieCollection.Object);

			_settings.SetupGet(x => x.DefaultLanguage).Returns("en");

			// Act
			_languageManager = new LanguageManager(_settings.Object, _context.Object);

			// Assert
			Assert.AreEqual("ru", _languageManager.Language);
		}

		[Test]
		public void SetCookieLanguage_EmptyLanguageString_CorrectCookieCreated()
		{
			// Act
			Assert.Throws<ArgumentNullException>(() => _languageManager.SetCookieLanguage(null));
		}

		[Test]
		public void SetCookieLanguage_CorrectLanguage_CorrectCookieCreated()
		{
			// Assign

			_settings.SetupGet(x => x.DefaultLanguage).Returns("en");
			_languageManager = new LanguageManager(_settings.Object, _context.Object);

			_responseCookies.Setup(x => x.Append(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((key, value) =>
			{
				Assert.AreEqual("Set-Cookie", key);
				Assert.IsTrue(value.Contains("language=ru"));
			});

			// Act
			_languageManager.SetCookieLanguage("ru");
		}

		[Test]
		public void Constructor_HaveBrowserLanguageAndSettingIsEnabledCase1_LanguageSetFromHeader()
		{
			// Assign

			_settings.SetupGet(x => x.AcceptBrowserLanguage).Returns(true);
			var header = new HeaderDictionary(new Dictionary<string, StringValues>());
			header.Append("Accept-Language", "ru-RU");
			_context.SetupGet(x => x.Request.Headers).Returns(header);

			// Act
			_languageManager = new LanguageManager(_settings.Object, _context.Object);

			// Assert
			Assert.AreEqual("ru", _languageManager.Language);
		}

		[Test]
		public void Constructor_HaveBrowserLanguageAndSettingIsEnabledCase2_LanguageSetFromHeader()
		{
			// Assign

			_settings.SetupGet(x => x.AcceptBrowserLanguage).Returns(true);
			var header = new HeaderDictionary(new Dictionary<string, StringValues>());
			header.Append("Accept-Language", "ru-RU;q=0.5");
			_context.SetupGet(x => x.Request.Headers).Returns(header);

			// Act
			_languageManager = new LanguageManager(_settings.Object, _context.Object);

			// Assert
			Assert.AreEqual("ru", _languageManager.Language);
		}

		[Test]
		public void Constructor_HaveBrowserLanguageAndCookieLanguage_LanguageSetFromCookie()
		{
			// Assign

			var cookieCollection = new Mock<IRequestCookieCollection>();
			cookieCollection.SetupGet(x => x[It.Is<string>(s => s == LanguageManager.CookieLanguageFieldName)]).Returns("fr");
			_context.SetupGet(x => x.Request.Cookies).Returns(cookieCollection.Object);

			_settings.SetupGet(x => x.AcceptBrowserLanguage).Returns(true);
			var header = new HeaderDictionary(new Dictionary<string, StringValues>());
			header.Append("Accept-Language", "ru-RU");
			_context.SetupGet(x => x.Request.Headers).Returns(header);

			// Act
			_languageManager = new LanguageManager(_settings.Object, _context.Object);

			// Assert
			Assert.AreEqual("fr", _languageManager.Language);
		}

		[Test]
		public void Constructor_NoBrowserLanguage_DefaultLanguageSet()
		{
			// Assign

			_settings.SetupGet(x => x.AcceptBrowserLanguage).Returns(true);
			var header = new HeaderDictionary(new Dictionary<string, StringValues>());
			_context.SetupGet(x => x.Request.Headers).Returns(header);

			// Act
			_languageManager = new LanguageManager(_settings.Object, _context.Object);

			// Assert
			Assert.AreEqual("en", _languageManager.Language);
		}
	}
}