using System;
using System.Threading.Tasks;

namespace Simplify.Web.Tests.Old.Core.Controllers.TestTypes;

public class TestController2 : AsyncController
{
	public override Task<ControllerResponse?> Invoke()
	{
		throw new NotImplementedException();
	}
}