using System;
using Simplify.Web.Attributes;

namespace Simplify.Web.Tests.TestEntities;

[Get("/testaction")]
public class TestControllerV2 : Controller2
{
	public ControllerResponse Invoke() => throw new NotImplementedException();
}