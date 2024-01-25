using System;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class ServerRackSystem: AwakeSystem<ServerRack, GameObject, ServerRackConfig>
    {
        public override void Awake(ServerRack self, GameObject a, ServerRackConfig serverRackConfig)
        {
            self.Awake(a, serverRackConfig);
        }
    }
}