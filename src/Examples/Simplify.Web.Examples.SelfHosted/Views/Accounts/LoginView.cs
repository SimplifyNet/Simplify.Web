using Simplify.Templates;
using Simplify.Web.Examples.SelfHosted.Models.Accounts;

namespace Simplify.Web.Examples.SelfHosted.Views.Accounts;

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