using System;
using System.Collections.Generic;
using Simplify.DI;

namespace Simplify.Web.Bootstrapper
{
	public class SimplifyWebRegistrationOverride
	{
		private IList<Action<IDIRegistrator>> Actions = new List<Action<IDIRegistrator>>();

		public SimplifyWebRegistrationOverride OverrideConfiguration(Action<IDIRegistrator> registrator)
		{
			Actions.Add(registrator);

			return this;
		}

		public SimplifyWebRegistrationOverride OverrideControllerExecutor(Action<IDIRegistrator> registrator)
		{
			Actions.Add(registrator);

			return this;
		}

		public void RegisterActions(IDIRegistrator registrator)
		{
			foreach (var item in Actions)
				item.Invoke(registrator);
		}
	}
}