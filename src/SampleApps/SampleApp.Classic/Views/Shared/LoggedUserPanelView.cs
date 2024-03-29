﻿using System.Threading.Tasks;
using Simplify.Templates;
using Simplify.Web;

namespace SampleApp.Classic.Views.Shared;

public class LoggedUserPanelView : View
{
	public async Task<ITemplate> Get(string userName) =>
		(await TemplateFactory.LoadAsync("Shared/LoginPanel/LoggedUserPanel"))
			.Add("UserName", userName);
}