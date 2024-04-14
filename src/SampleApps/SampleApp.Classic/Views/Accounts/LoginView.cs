using SampleApp.Classic.Models.Accounts;
using Simplify.Templates;
using Simplify.Templates.Model;
using Simplify.Web.Old;

namespace SampleApp.Classic.Views.Accounts;

public class LoginView : View
{
	public ITemplate Get(LoginViewModel viewModel = null, string message = null) =>
		TemplateFactory.Load("Accounts/LoginPage")
			.Model(viewModel)
			.With(x => x.RememberMe, x => x ? "checked='checked'" : "")
			.Set().Set("Message", message);
}