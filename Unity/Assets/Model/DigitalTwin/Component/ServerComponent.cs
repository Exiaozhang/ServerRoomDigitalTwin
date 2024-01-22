using System;
using System.Collections.Generic;
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

        public Server AddServer(Int32 id, Int32 position)
        {
            Server server = ComponentFactory.CreateWithParent<Server, Int32, Int32>(this, id, position);
            this.servers.Add(id, server);
            return server;
        }
    }
}