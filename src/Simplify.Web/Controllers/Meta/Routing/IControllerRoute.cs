using System.Collections.Generic;

namespace Simplify.Web.Controllers.Meta.Routing;

public interface IControllerRoute
{
	string Path { get; }

	/// <summary>
	/// Gets the controller path items.
	/// </summary>
	IList<PathItem> Items { get; }
}