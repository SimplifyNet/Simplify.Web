using System;
using Simplify.Web.Attributes;

namespace Simplify.Web.Examples.SelfHosted.Controllers;

[Get("exception")]
public class ExceptionExampleController : Controller
{
	public override ControllerResponse Invoke()
	{
		throw new NotImplementedException();
	}
}