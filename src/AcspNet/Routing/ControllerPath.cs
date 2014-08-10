﻿using System.Collections.Generic;

namespace AcspNet.Routing
{
	/// <summary>
	/// Provides parsed controller path
	/// </summary>
	public class ControllerPath : IControllerPath
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerPath"/> class.
		/// </summary>
		/// <param name="items">The items.</param>
		public ControllerPath(IList<IPathItem> items)
		{
			Items = items;
		}

		/// <summary>
		/// Gets the controller path items.
		/// </summary>
		/// <value>
		/// The controller path items.
		/// </value>
		public IList<IPathItem> Items { get; private set; }
	}
}