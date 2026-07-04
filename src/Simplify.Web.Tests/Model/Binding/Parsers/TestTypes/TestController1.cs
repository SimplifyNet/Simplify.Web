using System;
using Simplify.Web.Attributes;

namespace Simplify.Web.Tests.Model.Binding.Parsers.TestTypes;

[Get("/testaction")]
[Post("/testaction1")]
[Put("/testaction2")]
[Patch("/testaction3")]
[Delete("/testaction4")]
[Options("/testaction5")]
[Query("/testaction6")]
[Http403]
[Http404]
[Priority(1)]
[Authorize("Admin, User")]
public class TestController1 : Controller
{
	public override ControllerResponse Invoke() => throw new NotImplementedException();
}