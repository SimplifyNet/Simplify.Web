﻿using Simplify.Web.Model.Validation.Attributes;

namespace SampleApp.Classic.Models.Accounts
{
	public class LoginViewModel
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		public string Password { get; set; }

		public bool RememberMe { get; set; }
	}
}