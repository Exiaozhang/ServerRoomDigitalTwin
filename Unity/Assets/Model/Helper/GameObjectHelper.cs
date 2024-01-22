using System;
using UnityEngine;

namespace ETModel
{
	public static class GameObjectHelper
	{
		/// <summary>
		/// 获取GameObejct的RederenceCollector组件引用的类型
		/// </summary>
		/// <param name="gameObject"></param>
		/// <param name="key"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public static T Get<T>(this GameObject gameObject, string key) where T : class
		{
			try
			{
				return gameObject.GetComponent<ReferenceCollector>().Get<T>(key);
			}
			catch (Exception e)
			{
				throw new Exception($"获取{gameObject.name}的ReferenceCollector key失败, key: {key}", e);
			}
		}
	}
}