using System;

namespace Simplify.Web.Controllers.V1.Metadata;

public static class Controller1Types
{
	public static Type[] Types =>
	[
		typeof(Controller),
		typeof(AsyncController),
		typeof(Controller<>),
		typeof(AsyncController<>),
	];
}