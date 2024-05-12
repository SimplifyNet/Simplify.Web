using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework;
using Simplify.Web.Modules.Localization;
using Simplify.Web.Settings;

namespace Simplify.Web.Tests.Modules;

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
		_responseCookies = new Mock<IResponseCookies>();

		_settings.SetupGet(x => x.DefaultLanguage).Returns("en");

		_context.SetupGet(x => x.Request.Cookies).Returns(Mock.Of<IRequestCookieCollection>());
		_context.SetupGet(x => x.Response.Cookies).Returns(_responseCookies.Object);

		_languageManager = new LanguageManager(_settings.Object, _context.Object);
	}

	[Test]
	public void Constructor_NoRequestCookieLanguageAndCookieLanguageIsEnabled_DefaultLanguageSet()
	{
		// Arrange
		_settings.SetupGet(x => x.AcceptCookieLanguage).Returns(true);

		// Assert
		Assert.That(_languageManager.Language, Is.EqualTo("en"));
	}

	[Test]
	public void Constructor_HaveRequestCookieLanguageAndCookieLanguageIsEnabled_CurrentLanguageSet()
	{
		// Assign

		var cookieCollection = new Mock<IRequestCookieCollection>();

		cookieCollection.SetupGet(x => x[It.Is<string>(s => s == LanguageManager.CookieLanguageFieldName)]).Returns("ru");

		_settings.SetupGet(x => x.AcceptCookieLanguage).Returns(true);
		_settings.SetupGet(x => x.DefaultLanguage).Returns("en");

		_context.SetupGet(x => x.Request.Cookies).Returns(cookieCollection.Object);

		// Act
		_languageManager = new LanguageManager(_settings.Object, _context.Object);

		// Assert
		Assert.That(_languageManager.Language, Is.EqualTo("ru"));
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

		_languageManager = new LanguageManager(_settings.Object, _context.Object);

		_settings.SetupGet(x => x.DefaultLanguage).Returns("en");

		_responseCookies.Setup(x => x.Append(It.IsAny<string>(), It.IsAny<string>())).Callback<string, string>((key, value) =>
		{
			Assert.That(key, Is.EqualTo("Set-Cookie"));
			Assert.That(value.Contains("language=ru"), Is.True);
		});

		// Act
		_languageManager.SetCookieLanguage("ru");
	}

	[Test]
	public void Constructor_HaveHeaderLanguageAndSettingIsEnabledCase1_LanguageSetFromHeader()
	{
		// Assign

		var header = new HeaderDictionary([]);

		header.Append("Accept-Language", "ru-RU");

		_settings.SetupGet(x => x.AcceptHeaderLanguage).Returns(true);
		_context.SetupGet(x => x.Request.Headers).Returns(header);

		// Act
		_languageManager = new LanguageManager(_settings.Object, _context.Object);

		// Assert
		Assert.That(_languageManager.Language, Is.EqualTo("ru"));
	}

	[Test]
	public void Constructor_HaveHeaderLanguageAndSettingIsEnabledCase2_LanguageSetFromHeader()
	{
		// Assign

		var header = new HeaderDictionary([]);

		header.Append("Accept-Language", "ru-RU;q=0.5");

		_settings.SetupGet(x => x.AcceptHeaderLanguage).Returns(true);
		_context.SetupGet(x => x.Request.Headers).Returns(header);

		// Act
		_languageManager = new LanguageManager(_settings.Object, _context.Object);

		// Assert
		Assert.That(_languageManager.Language, Is.EqualTo("ru"));
	}

	[Test]
	public void Constructor_HaveHeaderLanguageAndCookieLanguageAndCookieLanguageIsEnabled_LanguageSetFromCookie()
	{
		// Assign

		var cookieCollection = new Mock<IRequestCookieCollection>();
		var header = new HeaderDictionary([]);

		header.Append("Accept-Language", "ru-RU");

		cookieCollection.SetupGet(x => x[It.Is<string>(s => s == LanguageManager.CookieLanguageFieldName)]).Returns("fr");

		_settings.SetupGet(x => x.AcceptCookieLanguage).Returns(true);
		_settings.SetupGet(x => x.AcceptHeaderLanguage).Returns(true);

		_context.SetupGet(x => x.Request.Cookies).Returns(cookieCollection.Object);
		_context.SetupGet(x => x.Request.Headers).Returns(header);

		// Act
		_languageManager = new LanguageManager(_settings.Object, _context.Object);

		// Assert
		Assert.That(_languageManager.Language, Is.EqualTo("fr"));
	}

	[Test]
	public void Constructor_HaveHeaderLanguageAndCookieLanguageAndCookieLanguageIsDisabled_LanguageSetFromHeader()
	{
		// Assign

		var cookieCollection = new Mock<IRequestCookieCollection>();
		var header = new HeaderDictionary([]);

		header.Append("Accept-Language", "ru-RU");

		cookieCollection.SetupGet(x => x[It.Is<string>(s => s == LanguageManager.CookieLanguageFieldName)]).Returns("fr");

		_settings.SetupGet(x => x.AcceptHeaderLanguage).Returns(true);

		_context.SetupGet(x => x.Request.Cookies).Returns(cookieCollection.Object);
		_context.SetupGet(x => x.Request.Headers).Returns(header);

		// Act
		_languageManager = new LanguageManager(_settings.Object, _context.Object);

		// Assert
		Assert.That(_languageManager.Language, Is.EqualTo("ru"));
	}

	[Test]
	public void Constructor_NoHeaderLanguage_DefaultLanguageSet()
	{
		// Assign

		var header = new HeaderDictionary([]);

		_settings.SetupGet(x => x.AcceptHeaderLanguage).Returns(true);
		_context.SetupGet(x => x.Request.Headers).Returns(header);

		// Act
		_languageManager = new LanguageManager(_settings.Object, _context.Object);

		// Assert
		Assert.That(_languageManager.Language, Is.EqualTo("en"));
	}

	[Test]
	public void Constructor_BadDefaultLanguage_InvariantLanguageSet()
	{
		// Arrange
		_settings.SetupGet(x => x.DefaultLanguage).Returns("bad-language");

		// Act
		_languageManager = new LanguageManager(_settings.Object, _context.Object);

		// Assert
		Assert.That(_languageManager.Language, Is.EqualTo("iv"));
	}
}