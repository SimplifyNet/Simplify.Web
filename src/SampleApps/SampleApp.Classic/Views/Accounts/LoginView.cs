using SampleApp.Classic.Models.Accounts;
using Simplify.Templates;
using Simplify.Web;

namespace SampleApp.Classic.Views.Accounts
{
	public class LoginView : View
	{
		public ITemplate Get(LoginViewModel viewModel = null, string message = null)
		{
			return
				TemplateFactory.Load("Accounts/LoginPage")
					.Model(viewModel)
					.With(x => x.RememberMe, x => x ? "checked='checked'" : "")
					.Set().Set("Message", message);
		}
	}
}