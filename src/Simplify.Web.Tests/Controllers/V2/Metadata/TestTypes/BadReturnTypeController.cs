namespace Simplify.Web.Tests.Controllers.V2.Metadata.TestTypes;

public class BadReturnTypeController : Controller2
{
	public object? Invoke() => null;
}