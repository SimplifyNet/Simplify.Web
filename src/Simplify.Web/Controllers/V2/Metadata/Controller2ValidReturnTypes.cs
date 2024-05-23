using System;
using System.Threading.Tasks;

namespace Simplify.Web.Controllers.V2.Metadata;

public static class Controller2ValidReturnTypes
{
	public static Type[] Types =>
	[
		typeof(void),
		typeof(ControllerResponse),
		typeof(Task),
		typeof(Task<ControllerResponse>)
	];
}