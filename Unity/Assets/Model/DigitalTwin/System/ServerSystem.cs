using System;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class ServerAwakeSystem: AwakeSystem<Server, GameObject, ServerConfig>
    {
        public override void Awake(Server self, GameObject serverObj, ServerConfig serverConfig)
        {
            self.Awake(serverObj, serverConfig);
        }
    }
}