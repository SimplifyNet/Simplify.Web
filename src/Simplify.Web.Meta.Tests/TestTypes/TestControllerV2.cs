using System;
using Simplify.Web.Attributes;

namespace Simplify.Web.Meta.Tests.TestTypes;

[Get("/testaction")]
public class TestControllerV2 : Controller2
{
	public ControllerResponse Invoke() => throw new NotImplementedException();
}