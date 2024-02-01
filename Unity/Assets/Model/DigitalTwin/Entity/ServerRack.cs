using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETModel
{
    /// <summary>
    /// 机架,机架由上面的服务器和交换机组成等等
    /// 此Enitity由ServerComponent构成
    /// </summary>
    public class ServerRack: DigitTwinObject
    {
        /// <summary>
        /// 机架温度
        /// </summary>
        /// <value></value>
        public Single Temperature { get; set; }

        public Int32 Id { get; set; }

        public Int32 Position { get; set; }

        public String Name { get; set; }

        public ServerRackInteraction Interaction { get; protected set; }

        public void Awake(GameObject gameObj, ServerRackConfig serverRackConfig)
        {
            Temperature = 0;
            this.GameObject = UnityEngine.Object.Instantiate(gameObj);
            this.Id = serverRackConfig.Id;
            this.Position = serverRackConfig.Position;
            this.AddComponent<ServerComponent>();

            //添加交互功能
            this.Interaction = this.GameObject.AddComponent<ServerRackInteraction>();
            this.Interaction.serverRack = this;
        }

        /// <summary>
        /// 在场景中将Server的Gameobject添加到ServerRack上
        /// </summary>
        public void AddServer(GameObject gameObject, Int32 position)
        {
            ReferenceCollector referenceCollector = this.GameObject.GetComponent<ReferenceCollector>();
            Transform transform = referenceCollector.Get<GameObject>(position.ToString()).GetComponent<Transform>();
            gameObject.transform.position = transform.position;
            gameObject.transform.rotation = transform.rotation;
            gameObject.transform.parent = this.GameObject.transform;
        }

        /// <summary>
        /// 设置服务器在机架中的位置
        /// </summary>
        private void SetPosition(Server server)
        {
            ReferenceCollector referenceCollector = this.GameObject.GetComponent<ReferenceCollector>();
            Transform transform = referenceCollector.Get<GameObject>(server.Position.ToString()).GetComponent<Transform>();
            server.GameObject.transform.position = transform.position;
            server.GameObject.transform.rotation = transform.rotation;
            server.GameObject.transform.parent = this.GameObject.transform;
        }

        /// <summary>
        /// 在场景中将Server的Gameobject添加到ServerRack上
        /// </summary>
        public Server AddServer(ServerConfig serverConfig)
        {
            GameObject serverObj = Resources.Load<GameObject>("DigitTwin/Server");
            Server server = ComponentFactory.CreateWithParent<Server, GameObject, ServerConfig>(this, serverObj, serverConfig);

            //加载到serversComponent种管理
            ServerComponent serverComponent = this.GetComponent<ServerComponent>();
            serverComponent.AddServer(server.Id, server);

            //设置位置
            this.SetPosition(server);

            return server;
        }
    }
}