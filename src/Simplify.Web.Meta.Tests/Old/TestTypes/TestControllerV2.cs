using System;
using Simplify.Web.Old;
using Simplify.Web.Old.Attributes;

namespace Simplify.Web.Meta.Tests.Old.TestTypes;

[Get("/test-action")]
public class TestControllerV2 : Controller2
{
	public ControllerResponse Invoke() => throw new NotImplementedException();
}