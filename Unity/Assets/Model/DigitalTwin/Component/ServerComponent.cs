using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ETModel
{
    /// <summary>
    /// 负责管理Server Entity
    /// </summary>
    public class ServerComponent: Component
    {
        private Dictionary<Int32, Server> servers;

        public void Awake()
        {
            this.servers = new Dictionary<Int32, Server>();
        }

        public Server AddServer(ServerConfig serverConfig)
        {
            //加载预制体资源
            GameObject serverObj = Resources.Load<GameObject>("DigitTwin/Server");
            Server server = ComponentFactory.CreateWithParent<Server, GameObject, ServerConfig>(this, serverObj, serverConfig);
            this.servers.Add(server.Id, server);
            return server;
        }


        public List<Server> GetAll()
        {
            return this.servers.Values.ToList();
        }
    }
}