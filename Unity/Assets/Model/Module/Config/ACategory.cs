using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ETModel
{
	public abstract class ACategory : Object
	{
		public abstract Type ConfigType { get; }
		public abstract IConfig GetOne();
		public abstract IConfig[] GetAll();
		public abstract IConfig TryGet(int type);
	}

	/// <summary>
	/// 管理该类型的所有配置
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class ACategory<T> : ACategory where T : IConfig
	{
		protected Dictionary<long, IConfig> dict;

		public override void BeginInit()
		{
			this.dict = new Dictionary<long, IConfig>();
			
			//根据类型名称找到Config预制体，上引用的文本资源
			string configStr = ConfigHelper.GetText(typeof(T).Name);

			foreach (string str in configStr.Split(new[] { "\n" }, StringSplitOptions.None))
			{
				try
				{
					string str2 = str.Trim();
					if (str2 == "")
					{
						continue;
					}
					//把json文本资源序列化成类
					T t = ConfigHelper.ToObject<T>(str2);

					//将配置放到字典中方便查找
					this.dict.Add(t.Id, t);
				}
				catch (Exception e)
				{
					throw new Exception($"parser json fail: {str}", e);
				}
			}
		}

		public override Type ConfigType
		{
			get
			{
				return typeof(T);
			}
		}

		public override void EndInit()
		{
		}

		public override IConfig TryGet(int type)
		{
			IConfig t;
			if (!this.dict.TryGetValue(type, out t))
			{
				return null;
			}
			return t;
		}

		public override IConfig[] GetAll()
		{
			return this.dict.Values.ToArray();
		}

		public override IConfig GetOne()
		{
			return this.dict.Values.First();
		}
	}
}