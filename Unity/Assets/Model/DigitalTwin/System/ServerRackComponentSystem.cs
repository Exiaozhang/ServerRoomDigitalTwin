using ETModel.Event;

namespace ETModel
{
    [ObjectSystem]
    public class ServerRackComponentSystem: AwakeSystem<ServerRackComponent>
    {
        public override void Awake(ServerRackComponent self)
        {
            self.Awake();
        }
    }
}