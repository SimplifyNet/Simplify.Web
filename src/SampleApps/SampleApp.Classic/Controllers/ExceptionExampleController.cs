using System;
using Simplify.Web;
using Simplify.Web.Attributes;

namespace SampleApp.Classic.Controllers;

[Get("exception")]
public class ExceptionExampleController : Controller2
{
	public ControllerResponse Invoke() => throw new NotImplementedException();
}