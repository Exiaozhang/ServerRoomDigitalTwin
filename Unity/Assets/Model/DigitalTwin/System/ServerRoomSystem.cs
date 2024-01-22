using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class ServerRoomSystem: AwakeSystem<ServerRoom, GameObject>
    {
        public override void Awake(ServerRoom self, GameObject a)
        {
            self.Awake(a);
        }
    }
}