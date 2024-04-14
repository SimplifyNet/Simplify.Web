using System.Collections.Generic;

namespace Simplify.Web.Old.Meta2;

/// <summary>
/// Represent controllers meta store.
/// </summary>
public interface IControllersMetaStore
{
	IList<IControllerMetaData> StandardControllers { get; }
	IList<IControllerMetaData> RoutedControllers { get; }
	IList<IControllerMetaData> GlobalControllers { get; }

	IControllerMetaData Controller400 { get; }
	IControllerMetaData Controller403 { get; }
	IControllerMetaData Controller404 { get; }
}