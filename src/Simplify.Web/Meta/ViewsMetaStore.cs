using System;
using System.Collections.Generic;

#nullable disable

namespace Simplify.Web.Meta
{
	/// <summary>
	/// Loads and stores views meta information
	/// </summary>
	public class ViewsMetaStore : IViewsMetaStore
	{
		private static IViewsMetaStore _current;
		private IList<Type> _viewsTypes;

		/// <summary>
		/// Current views meta store
		/// </summary>
		public static IViewsMetaStore Current
		{
			get
			{
				return _current ?? (_current = new ViewsMetaStore());
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException(nameof(value));

				_current = value;
			}
		}

		/// <summary>
		/// Current domain views types
		/// </summary>
		/// <returns></returns>
		public IList<Type> ViewsTypes
		{
			get
			{
				if (_viewsTypes != null)
					return _viewsTypes;

				_viewsTypes = SimplifyWebTypesFinder.FindTypesDerivedFrom<View>();

				return _viewsTypes;
			}
		}
	}
}