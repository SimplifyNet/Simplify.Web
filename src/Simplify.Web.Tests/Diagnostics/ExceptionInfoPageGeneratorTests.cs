using System;
using NUnit.Framework;
using Simplify.Web.Diagnostics;

namespace Simplify.Web.Tests.Diagnostics;

[TestFixture]
public class ExceptionInfoPageGeneratorTests
{
	[Test]
	public void Generate_WithInnerException_HtmlPageText()
	{
		try
		{
			string? text = null;
			// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
			text!.IndexOf("test", StringComparison.Ordinal);
		}
		catch (Exception e)
		{
			try
			{
				throw new Exception("test 2", e);
			}
			catch (Exception ex)
			{
				Assert.That(ErrorPageGenerator.Generate(ex).Contains("html"), Is.True);
			}
		}
	}

	[Test]
	public void Generate_ExceptionNoFrames_Generated()
	{
		Assert.That(ErrorPageGenerator.Generate(new Exception("test")), Is.Not.Null);
	}

	[Test]
	public void Generate_HideDetails_Null()
	{
		Assert.That(ErrorPageGenerator.Generate(new Exception("test"), true).Contains("html"), Is.True);
	}
}