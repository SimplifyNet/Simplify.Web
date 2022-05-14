using System.Threading.Tasks;
using Simplify.Templates;
using Simplify.Web;

namespace SampleApp.Classic.Views.Shared;

public class LoggedUserPanelView : View
{
	public async Task<ITemplate> Get(string userName)
	{
		var tpl = await TemplateFactory.LoadAsync("Shared/LoginPanel/LoggedUserPanel");

		tpl.Add("UserName", userName);

		return tpl;
	}
}