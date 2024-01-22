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
        /// 添加机架
        /// </summary>
        /// <param name="id">机架的id</param>
        /// <param name="position">机架的position</param>
        /// <returns></returns>
        public ServerRack AddServerRack(Int32 id, Int32 position)
        {
            GameObject serverRackObj = Resources.Load<GameObject>("DigitTwin/ServerRack");
            ServerRack serverRack = ComponentFactory.CreateWithParent<ServerRack, GameObject, Int32, Int32>(this, serverRackObj, id, position);
            serverRacks.Add(id, serverRack);
            return serverRack;
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
    }
}