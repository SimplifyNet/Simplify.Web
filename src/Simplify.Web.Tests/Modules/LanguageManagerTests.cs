using System;
using System.Collections.Generic;
using Microsoft.Owin;
using Moq;
using NUnit.Framework;
using Simplify.Web.Modules;
using Simplify.Web.Settings;

namespace Simplify.Web.Tests.Modules;

[TestFixture]
public class LanguageManagerTests
{
	private LanguageManager _languageManager;
	private Mock<ISimplifyWebSettings> _settings;
	private Mock<IOwinContext> _context;
	private Mock<IHeaderDictionary> _headerDictionary;

	[SetUp]
	public void Initialize()
	{
		_settings = new Mock<ISimplifyWebSettings>();
		_context = new Mock<IOwinContext>();

		_settings.SetupGet(x => x.DefaultLanguage).Returns("en");
		_headerDictionary = new Mock<IHeaderDictionary>();

		_context.SetupGet(x => x.Request.Cookies).Returns(new RequestCookieCollection(new Dictionary<string, string>()));
		_context.SetupGet(x => x.Response.Cookies).Returns(new ResponseCookieCollection(_headerDictionary.Object));

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

		var cookies = new Dictionary<string, string> { { LanguageManager.CookieLanguageFieldName, "ru" } };
		_context.SetupGet(x => x.Request.Cookies).Returns(new RequestCookieCollection(cookies));
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

		_headerDictionary.Setup(x => x.AppendValues(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string[]>((key, values) =>
		{
			Assert.AreEqual("Set-Cookie", key);
			Assert.IsTrue(values[0].Contains("language=ru"));
		});

		// Act
		_languageManager.SetCookieLanguage("ru");
	}

	[Test]
	public void Constructor_HaveBrowserLanguageAndSettingIsEnabledCase1_LanguageSetFromHeader()
	{
		// Assign

		_settings.SetupGet(x => x.AcceptBrowserLanguage).Returns(true);
		var header = new HeaderDictionary(new Dictionary<string, string[]>());
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
		var header = new HeaderDictionary(new Dictionary<string, string[]>());
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

		var cookies = new Dictionary<string, string> { { LanguageManager.CookieLanguageFieldName, "fr" } };
		_context.SetupGet(x => x.Request.Cookies).Returns(new RequestCookieCollection(cookies));

		_settings.SetupGet(x => x.AcceptBrowserLanguage).Returns(true);
		var header = new HeaderDictionary(new Dictionary<string, string[]>());
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
		var header = new HeaderDictionary(new Dictionary<string, string[]>());
		_context.SetupGet(x => x.Request.Headers).Returns(header);

		// Act
		_languageManager = new LanguageManager(_settings.Object, _context.Object);

		// Assert
		Assert.AreEqual("en", _languageManager.Language);
	}
}