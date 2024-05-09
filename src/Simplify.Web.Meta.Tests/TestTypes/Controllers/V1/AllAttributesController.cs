using System;
using Simplify.Web.Attributes;

namespace Simplify.Web.Meta.Tests.TestTypes.Controllers.V1;

[Get("/test-action")]
[Post("/test-action1")]
[Put("/test-action2")]
[Patch("/test-action3")]
[Delete("/test-action4")]
[Options("/test-action5")]
[Http403]
[Http404]
[Priority(1)]
[Authorize("Admin, User")]
public class AllAttributesController : Controller
{
	public override ControllerResponse Invoke() => throw new NotImplementedException();
}