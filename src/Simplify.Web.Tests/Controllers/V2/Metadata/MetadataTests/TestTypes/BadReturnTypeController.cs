namespace Simplify.Web.Tests.Controllers.V2.Metadata.MetadataTests.TestTypes;

#pragma warning disable CA1822 // Mark members as static

public class BadReturnTypeController : Controller2
{
	public object? Invoke() => null;
}

#pragma warning restore CA1822 // Mark members as static