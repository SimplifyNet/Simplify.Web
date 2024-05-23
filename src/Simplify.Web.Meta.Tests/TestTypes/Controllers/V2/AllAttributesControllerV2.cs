using System;
using Simplify.Web.Attributes;

namespace Simplify.Web.Meta.Tests.TestTypes.Controllers.V2;

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
public class AllAttributesControllerV2 : Controller2
{
	public ControllerResponse Invoke() => throw new NotImplementedException();
}