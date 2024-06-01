using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Web.System;

namespace Simplify.Web.Views.Meta;

/// <summary>
/// Provides the views meta information store.
/// </summary>
/// <seealso cref="IViewsMetaStore" />
public class ViewsMetaStore : IViewsMetaStore
{
	private static IViewsMetaStore? _current;
	private IList<Type>? _viewsTypes;

	/// <summary>
	/// Gets the current views meta store.
	/// </summary>
	/// <value>
	/// The current.
	/// </value>
	/// <exception cref="ArgumentNullException">value</exception>
	public static IViewsMetaStore Current
	{
		get => _current ??= new ViewsMetaStore();
		set => _current = value ?? throw new ArgumentNullException(nameof(value));
	}

	/// <summary>
	/// Gets the current domain views types.
	/// </summary>
	/// <value>
	/// The views types list.
	/// </value>
	public IList<Type> ViewsTypes
	{
		get
		{
			if (_viewsTypes != null)
				return _viewsTypes;

			_viewsTypes = SimplifyWebTypesFinder.FindTypesDerivedFrom<View>().ToList();

			return _viewsTypes;
		}
	}
}