#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Attributes.Setup;

namespace Simplify.Web.Meta
{
	/// <summary>
	/// Loads and stores controllers meta information
	/// </summary>
	public class ControllersMetaStore : IControllersMetaStore
	{
		private static IControllersMetaStore _current;
		private readonly IControllerMetaDataFactory _metaDataFactory;
		private IList<IControllerMetaData> _controllersMetaData;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllersMetaStore"/> class.
		/// </summary>
		/// <param name="metaDataFactory">The meta data factory.</param>
		public ControllersMetaStore(IControllerMetaDataFactory metaDataFactory)
		{
			_metaDataFactory = metaDataFactory;
		}

		/// <summary>
		/// Current controllers meta store
		/// </summary>
		public static IControllersMetaStore Current
		{
			get
			{
				return _current ?? (_current = new ControllersMetaStore(new ControllerMetaDataFactory()));
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException(nameof(value));

				_current = value;
			}
		}

		/// <summary>
		/// Get controllers meta-data
		/// </summary>
		/// <returns></returns>
		public IList<IControllerMetaData> ControllersMetaData
		{
			get
			{
				if (_controllersMetaData != null)
					return _controllersMetaData;

				var controllersMetaContainers = new List<IControllerMetaData>();

				var types = SimplifyWebTypesFinder.FindTypesDerivedFrom<Controller>();
				types = types.Concat(SimplifyWebTypesFinder.FindTypesDerivedFrom<AsyncController>()).ToList();
				types = types.Concat(SimplifyWebTypesFinder.FindTypesDerivedFrom(typeof(Controller<>))).ToList();
				types = types.Concat(SimplifyWebTypesFinder.FindTypesDerivedFrom(typeof(AsyncController<>))).ToList();

				var typesToIgnore = new List<Type>();

				var ignoreContainingClass =
					SimplifyWebTypesFinder.GetAllTypes().FirstOrDefault(t => t.IsDefined(typeof(IgnoreControllersAttribute), true));

				if (ignoreContainingClass != null)
				{
					var attributes = ignoreContainingClass.GetCustomAttributes(typeof(IgnoreControllersAttribute), false);

					typesToIgnore.AddRange(((IgnoreControllersAttribute)attributes[0]).Types);
				}

				LoadMetaData(controllersMetaContainers, types, typesToIgnore);
				_controllersMetaData = SortControllersMetaContainers(controllersMetaContainers);

				return _controllersMetaData;
			}
		}

		private static IList<IControllerMetaData> SortControllersMetaContainers(IEnumerable<IControllerMetaData> controllersMetaContainers)
		{
			return controllersMetaContainers.OrderBy(x => x.ExecParameters?.RunPriority ?? 0).ToList();
		}

		private void LoadMetaData(ICollection<IControllerMetaData> controllersMetaContainers, IEnumerable<Type> types, IEnumerable<Type> typesToIgnore)
		{
			foreach (var t in types.Where(t => typesToIgnore.All(x => x.FullName != t.FullName) &&
											   controllersMetaContainers.All(x => x.ControllerType != t)))
			{
				BuildControllerMetaData(controllersMetaContainers, t);
			}
		}

		private void BuildControllerMetaData(ICollection<IControllerMetaData> controllersMetaContainers, Type controllerType)
		{
			controllersMetaContainers.Add(_metaDataFactory.CreateControllerMetaData(controllerType));
		}
	}
}