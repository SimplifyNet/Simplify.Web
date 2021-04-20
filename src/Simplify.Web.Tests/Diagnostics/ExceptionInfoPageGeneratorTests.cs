using System;
using NUnit.Framework;
using Simplify.Web.Diagnostics;

namespace Simplify.Web.Tests.Diagnostics
{
	[TestFixture]
	public class ExceptionInfoPageGeneratorTests
	{
		[Test]
		public void Generate_WithInnerException_HtmlPageText()
		{
			try
			{
				string? text = null;
				// ReSharper disable PossibleNullReferenceException
				text!.IndexOf("test", StringComparison.Ordinal);
				// ReSharper restore PossibleNullReferenceException
			}
			catch (Exception e)
			{
				try
				{
					throw new Exception("test 2", e);
				}
				catch (Exception ex)
				{
					Assert.IsTrue(ErrorPageGenerator.Generate(ex).Contains("html"));
				}
			}
		}

		[Test]
		public void Generate_ExceptionNoFrames_Generated()
		{
			Assert.IsNotNull(ErrorPageGenerator.Generate(new Exception("test")));
		}

		[Test]
		public void Generate_HideDetails_Null()
		{
			Assert.IsTrue(ErrorPageGenerator.Generate(new Exception("test"), true).Contains("html"));
		}
	}
}