using System.Collections.Generic;

namespace Simplify.Web.Old.Meta;

/// <summary>
/// Represent controllers meta store.
/// </summary>
public interface IControllersMetaStore
{
	/// <summary>
	/// Current domain controllers meta-data
	/// </summary>
	/// <returns></returns>
	IList<IControllerMetaData> ControllersMetaData { get; }
}