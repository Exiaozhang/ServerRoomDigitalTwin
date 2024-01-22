using System;
using UnityEngine;

namespace ETModel
{
	/// <summary>
	/// 负责读取AssestBunle和Resource上预制体保存的配置
	/// </summary>
	public static class ConfigHelper
	{
		/// <summary>
		/// 获取Config预制体上引用的文本资源
		/// </summary>
		/// <param name="key">ReferenceCollector中的Key</param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public static string GetText(string key)
		{
			try
			{	
				//加载AssetBundle中Config的预制体
				GameObject config = (GameObject)Game.Scene.GetComponent<ResourcesComponent>().GetAsset("config.unity3d", "Config");
				//获取预制体上引用的文本资源
				string configStr = config.Get<TextAsset>(key).text;
				return configStr;
			}
			catch (Exception e)
			{
				throw new Exception($"load config file fail, key: {key}", e);
			}
		}
		
		/// <summary>
		/// 得到资源中KV预制体上引用的GlobalProto(网关服务器和资源服务器的ip和port)文本资源
		/// </summary>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public static string GetGlobal()
		{
			try
			{
				GameObject config = (GameObject)ResourcesHelper.Load("KV");
				string configStr = config.Get<TextAsset>("GlobalProto").text;
				return configStr;
			}
			catch (Exception e)
			{
				throw new Exception($"load global config file fail", e);
			}
		}

		/// <summary>
		/// 将Json文件序列为为Object
		/// </summary>
		/// <param name="str"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T ToObject<T>(string str)
		{
			return JsonHelper.FromJson<T>(str);
		}
	}
}