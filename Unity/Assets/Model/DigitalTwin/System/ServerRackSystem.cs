using System;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class ServerRackSystem: AwakeSystem<ServerRack, GameObject, Int32, Int32>
    {
        public override void Awake(ServerRack self, GameObject a, Int32 id, Int32 position)
        {
            self.Awake(a, id, position);
        }
    }
}