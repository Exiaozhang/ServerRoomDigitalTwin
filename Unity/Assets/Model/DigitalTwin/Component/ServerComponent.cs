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

        public void AddServer(Int32 id, Server server)
        {
            //加载预制体资源
            servers.Add(id, server);
        }

        public List<Server> GetAll()
        {
            return this.servers.Values.ToList();
        }
    }
}