using System;
using JetBrains.Annotations;
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

        public String Name { get; set; }

        /// <summary>
        /// 服务器所属的机架Id
        /// </summary>
        public Int32 RackId { get; set; }

        public ServerInteraction Interaction { get; set; }

        public void Awake(GameObject gameObject, ServerConfig serverConfig)
        {
            this.GameObject = UnityEngine.Object.Instantiate(gameObject);

            //初始化Server设置
            Temperature = 0;
            this.Position = serverConfig.Position;
            this.Id = serverConfig.Id;
            this.RackId = serverConfig.RackId;
            this.Name = serverConfig.Name;
            this.Interaction = this.GameObject.AddComponent<ServerInteraction>();
            Interaction.server = this;
        }
    }
}