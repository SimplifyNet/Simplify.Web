using System;
using Simplify.Web.Old.Attributes;

namespace Simplify.Web.Tests.Old.Model.Binding.Parsers.TestTypes;

[Get("/testaction")]
[Post("/testaction1")]
[Put("/testaction2")]
[Patch("/testaction3")]
[Delete("/testaction4")]
[Options("/testaction5")]
[Http400]
[Http403]
[Http404]
[Priority(1)]
[Authorize("Admin, User")]
public class TestController1 : Web.Old.Controller
{
	public override Web.Old.ControllerResponse Invoke() => throw new NotImplementedException();
}