using System;

namespace ETModel
{
    [ObjectSystem]
    public class ServerAwakeSystem: AwakeSystem<Server, Int32, Int32>
    {
        public override void Awake(Server self, Int32 id, Int32 position)
        {
            self.Awake(id, position);
        }
    }
}