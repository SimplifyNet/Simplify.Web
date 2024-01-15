using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Attributes.Setup;

namespace Simplify.Web.Meta;

/// <summary>
/// Loads and stores controllers meta information
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ControllersMetaStore"/> class.
/// </remarks>
/// <param name="metaDataFactory">The meta data factory.</param>
public class ControllersMetaStore(IControllerMetaDataFactory metaDataFactory) : IControllersMetaStore
{
	private static IControllersMetaStore? _current;
	private readonly IControllerMetaDataFactory _metaDataFactory = metaDataFactory;
	private IList<IControllerMetaData>? _controllersMetaData;

	/// <summary>
	/// Current controllers meta store
	/// </summary>
	public static IControllersMetaStore Current
	{
		get => _current ??= new ControllersMetaStore(new ControllerMetaDataFactory());
		set => _current = value ?? throw new ArgumentNullException(nameof(value));
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

			var ignoreContainingClass = SimplifyWebTypesFinder
											.GetAllTypes()
											.FirstOrDefault(t => t.IsDefined(typeof(IgnoreControllersAttribute), true));

			if (ignoreContainingClass != null)
			{
				var attributes = ignoreContainingClass.GetCustomAttributes(typeof(IgnoreControllersAttribute), false);

				typesToIgnore.AddRange(((IgnoreControllersAttribute)attributes[0]).Types);
			}

			LoadMetaData(controllersMetaContainers, types, typesToIgnore);

			return _controllersMetaData = controllersMetaContainers;
		}
	}

	private void LoadMetaData(ICollection<IControllerMetaData> controllersMetaContainers, IEnumerable<Type> types, IEnumerable<Type> typesToIgnore)
	{
		foreach (var t in types.Where(t => typesToIgnore.All(x => x.FullName != t.FullName) &&
										   controllersMetaContainers.All(x => x.ControllerType != t)))
			BuildControllerMetaData(controllersMetaContainers, t);
	}

	private void BuildControllerMetaData(ICollection<IControllerMetaData> controllersMetaContainers, Type controllerType) =>
		controllersMetaContainers.Add(_metaDataFactory.CreateControllerMetaData(controllerType));
}