﻿namespace CoreMvcTemplate.Models.Account
{
	public class LoginViewModel
	{
		public string UserName { get; set; }
		public string Password { get; set; }

		public string ReturnUrl { get; set; }
	}
}
