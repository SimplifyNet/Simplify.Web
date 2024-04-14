using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Web.Old.Attributes.Setup;

namespace Simplify.Web.Old.Meta;

/// <summary>
/// Loads and stores controllers meta information.
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

			return _controllersMetaData = LoadMetaData();
		}
	}

	private IList<IControllerMetaData> LoadMetaData()
	{
		var types = SimplifyWebTypesFinder.FindTypesDerivedFrom<Controller2>();
		types = types.Concat(SimplifyWebTypesFinder.FindTypesDerivedFrom(typeof(Controller2<>))).ToList();
		types = types.Concat(SimplifyWebTypesFinder.FindTypesDerivedFrom<Controller>()).ToList();
		types = types.Concat(SimplifyWebTypesFinder.FindTypesDerivedFrom<AsyncController>()).ToList();
		types = types.Concat(SimplifyWebTypesFinder.FindTypesDerivedFrom(typeof(Controller<>))).ToList();
		types = types.Concat(SimplifyWebTypesFinder.FindTypesDerivedFrom(typeof(AsyncController<>))).ToList();

		return LoadMetaData(types, GetTypesToIgnore());
	}

	private static IEnumerable<Type> GetTypesToIgnore()
	{
		var ignoreContainingClass = SimplifyWebTypesFinder
									.GetAllTypes()
									.FirstOrDefault(t => t.IsDefined(typeof(IgnoreControllersAttribute), true));

		if (ignoreContainingClass == null)
			return new List<Type>();

		var attributes = ignoreContainingClass.GetCustomAttributes(typeof(IgnoreControllersAttribute), false);

		return ((IgnoreControllersAttribute)attributes[0]).Types;
	}

	private IList<IControllerMetaData> LoadMetaData(IEnumerable<Type> types, IEnumerable<Type> typesToIgnore) =>
		types.Where(t => typesToIgnore.All(x => x.FullName != t.FullName))
			.Select(_metaDataFactory.CreateControllerMetaData)
			.ToList();
}