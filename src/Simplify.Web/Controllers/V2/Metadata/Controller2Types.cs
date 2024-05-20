using System;

namespace Simplify.Web.Controllers.V2.Metadata;

public static class Controller2Types
{
	public static Type[] Types =>
	[
		typeof(Controller2),
		typeof(Controller2<>)
	];
}