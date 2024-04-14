using System;
using Simplify.Web;
using Simplify.Web.Old;
using Simplify.Web.Old.Attributes;

namespace SampleApp.Classic.Controllers;

[Get("exception")]
public class ExceptionExampleController : Controller
{
	public override ControllerResponse Invoke() => throw new NotImplementedException();
}