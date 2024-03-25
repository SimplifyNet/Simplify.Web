using System;
using System.Threading.Tasks;

namespace Simplify.Web.Meta.Tests.TestTypes;

public class TestController2 : AsyncController
{
	public override Task<ControllerResponse?> Invoke()
	{
		throw new NotImplementedException();
	}
}