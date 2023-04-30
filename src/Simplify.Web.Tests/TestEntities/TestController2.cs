using System;
using System.Threading.Tasks;

namespace Simplify.Web.Tests.TestEntities;

public class TestController2 : AsyncController
{
#pragma warning disable 1998

	public override async Task<ControllerResponse> Invoke()
#pragma warning restore 1998
	{
		throw new NotImplementedException();
	}
}