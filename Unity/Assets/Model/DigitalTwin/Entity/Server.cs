using System;
using UnityEngine;

namespace ETModel
{
    public class Server: DigitTwinObject
    {
        /// <summary>
        /// 服务器温度
        /// </summary>
        /// <value></value>
        public Single Temperature { get; set; }

        public Int32 Id { get; set; }

        public Int32 Position { get; set; }

        public void Awake(Int32 id, Int32 position)
        {
            //加载预制体资源
            GameObject serverObj = Resources.Load<GameObject>("DigitTwin/Server");
            this.GameObject = UnityEngine.Object.Instantiate(serverObj);

            Temperature = 0;
            this.Id = id;
            this.Position = position;
        }
    }
}