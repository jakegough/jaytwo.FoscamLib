using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace jaytwo.FoscamLib
{
	public class FoscamHdClient
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Host { get; set; }
		public int Port { get; set; }
		public bool UseHttps { get; set; }

		public FoscamHdUrlBuilder CreateUrlBuilder()
		{
			var result = new FoscamHdUrlBuilder();
			result.Username = Username;
			result.Password = Password;
			result.Host = Host;
			result.Port = Port;
			result.UseHttps = UseHttps;

			return result;
		}

		private string GetCommandUrl(string command)
		{
			return CreateUrlBuilder().GetCommandUrl(command);
		}

		private string GetCommandUrl(string command, NameValueCollection parameters)
		{
			return CreateUrlBuilder().GetCommandUrl(command, parameters);
		}
	}
}
