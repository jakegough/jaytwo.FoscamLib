using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace jaytwo.FoscamLib.Test
{
	[TestClass]
	public class FoscamHdUrlBuilderTests
	{
		private static FoscamHdUrlBuilder GetTestFoscamHdUrlBuilder()
		{
			var result = new FoscamHdUrlBuilder();
			result.Host = "host";
			result.Port = 123;
			result.Username = "me@you.com";
			result.Password = "password";

			return result;
		}

		[TestMethod]
		public void FoscamHdUrlBuilder_GetRootUrl()
		{
			var builder = GetTestFoscamHdUrlBuilder();

			builder.UseHttps = false;
			Assert.AreEqual("http://host:123/", builder.GetRootUrl());
			Assert.AreEqual(builder.GetRootUrl(), builder.GetRootUri().AbsoluteUri);

			builder.UseHttps = true;
			Assert.AreEqual("https://host:123/", builder.GetRootUrl());
		}

		[TestMethod]
		public void FoscamHdUrlBuilder_GetCommandUrl()
		{
			var builder = GetTestFoscamHdUrlBuilder();

			var command = "hello";

			Assert.AreEqual("http://host:123/cgi-bin/CGIProxy.fcgi?cmd=hello&usr=me%40you.com&pwd=password", builder.GetCommandUrl(command));
		}

		[TestMethod]
		public void FoscamHdUrlBuilder_GetSnapshotUrlMainStream()
		{
			var builder = GetTestFoscamHdUrlBuilder();

			Assert.AreEqual("http://host:123/cgi-bin/CGIProxy.fcgi?cmd=snapPicture&usr=me%40you.com&pwd=password", builder.GetSnapshotUrlMainStream());
		}

		[TestMethod]
		public void FoscamHdUrlBuilder_GetSnapshotUrlSubStream()
		{
			var builder = GetTestFoscamHdUrlBuilder();

			Assert.AreEqual("http://host:123/cgi-bin/CGIProxy.fcgi?cmd=snapPicture2&usr=me%40you.com&pwd=password", builder.GetSnapshotUrlSubStream());
		}

		[TestMethod]
		public void FoscamHdUrlBuilder_GetVideoUrlMainStream()
		{
			var builder = GetTestFoscamHdUrlBuilder();

			var expectedWithCredentials = "rtsp://me%40you.com:password@host:123/videoMain";

			builder.UseHttps = false;
			Assert.AreEqual(expectedWithCredentials, builder.GetVideoUrlMainStream());

			builder.UseHttps = true;
			Assert.AreEqual(expectedWithCredentials, builder.GetVideoUrlMainStream());

			var expectedWithoutCredentials = "rtsp://host:123/videoMain";

			builder.UseHttps = false;
			Assert.AreEqual(expectedWithoutCredentials, builder.GetVideoUrlMainStream(false));

			builder.UseHttps = true;
			Assert.AreEqual(expectedWithoutCredentials, builder.GetVideoUrlMainStream(false));
		}

		[TestMethod]
		public void FoscamHdUrlBuilder_GetVideoUrlSubStream()
		{
			var builder = GetTestFoscamHdUrlBuilder();

			var expected = "rtsp://me%40you.com:password@host:123/videoSub";

			builder.UseHttps = false;
			Assert.AreEqual(expected, builder.GetVideoUrlSubStream());

			builder.UseHttps = true;
			Assert.AreEqual(expected, builder.GetVideoUrlSubStream());

			var expectedWithoutCredentials = "rtsp://host:123/videoSub";

			builder.UseHttps = false;
			Assert.AreEqual(expectedWithoutCredentials, builder.GetVideoUrlSubStream(false));

			builder.UseHttps = true;
			Assert.AreEqual(expectedWithoutCredentials, builder.GetVideoUrlSubStream(false));
		}
	}
}
