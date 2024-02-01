using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETModel
{
    /// <summary>
    /// 用于管理所有的ServerRack组件
    /// </summary>
    public class ServerRackComponent: Component
    {
        private Dictionary<Int32, ServerRack> serverRacks;

        public void Awake()
        {
            this.serverRacks = new Dictionary<Int32, ServerRack>();
        }

        /// <summary>
        /// 找到对应id的机架
        /// </summary>
        /// <param name="id"></param>
        /// <returns>如果没有发现则返回空值</returns>
        public ServerRack GetServerRack(Int32 id)
        {
            ServerRack res;
            if (this.serverRacks.TryGetValue(id, out res))
            {
                return res;
            }

            return null;
        }
        
        /// <summary>
        /// 添加机架到ServerRackComponent中
        /// </summary>
        /// <param name="id"></param>
        /// <param name="serverRack"></param>
        public void AddServerRack(Int32 id, ServerRack serverRack)
        {
            serverRacks.Add(id, serverRack);
        }
    }
}