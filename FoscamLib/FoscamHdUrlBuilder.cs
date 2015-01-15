using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace jaytwo.FoscamLib
{
	public class FoscamHdUrlBuilder
	{
		private bool DefaultIncludeCredentials = true;

		public string Username { get; set; }
		public string Password { get; set; }
		public string Host { get; set; }
		public int Port { get; set; }
		public bool UseHttps { get; set; }

		public string GetRootUrl()
		{
			var scheme = (UseHttps) ? "https" : "http";
			var result = string.Format("{0}://{1}:{2}/", scheme, Host, Port);
			return result;
		}

		public Uri GetRootUri()
		{
			var url = GetRootUrl();
			var result = new Uri(url);
			return result;
		}

		public string GetCommandUrl(string command)
		{
			return GetCommandUrl(command, null);
		}

		public string GetCommandUrl(string command, NameValueCollection parameters)
		{
			var rootUrl = GetRootUrl();
			var path = "cgi-bin/CGIProxy.fcgi";
			var baseUrl = rootUrl + path;

			var query = HttpUtility.ParseQueryString(string.Empty);
			query["cmd"] = command;
			query["usr"] = Username;
			query["pwd"] = Password;

			if (parameters != null)
			{
				query.Add(parameters);
			}

			var result = string.Format("{0}?{1}", baseUrl, query);
			return result;
		}

		public string GetSnapshotUrlMainStream()
		{
			return GetCommandUrl(FoscamHdUrlCommands.SnapPicture);
		}

		public string GetSnapshotUrlSubStream()
		{
			return GetCommandUrl(FoscamHdUrlCommands.SnapPicture2);
		}

		private string GetVideoStreamUrl(string streamName, bool includeCredentials)
		{
			var credentials = (includeCredentials) ? 
				string.Format("{0}:{1}@", HttpUtility.UrlEncode(Username), HttpUtility.UrlEncode(Password)) : 
				string.Empty;

			var scheme = "rtsp"; // rtsp is for both http and https

			var baseUrl = string.Format("{0}://{1}{2}:{3}/", 
				scheme, 
				credentials, 
				Host, 
				Port);

			var result = baseUrl + HttpUtility.UrlEncode(streamName);

			return result;
		}

		public string GetVideoUrlMainStream()
		{
			return GetVideoUrlMainStream(DefaultIncludeCredentials);
		}

		public string GetVideoUrlMainStream(bool includeCredentials)
		{
			var streamName = "videoMain";
			return GetVideoStreamUrl(streamName, includeCredentials);
		}

		public string GetVideoUrlSubStream()
		{
			return GetVideoUrlSubStream(DefaultIncludeCredentials);
		}

		public string GetVideoUrlSubStream(bool includeCredentials)
		{
			var streamName = "videoSub";
			return GetVideoStreamUrl(streamName, includeCredentials);
		}
	}
}
