namespace ETModel
{
    [ObjectSystem]
    public class ServerComponentSystem: AwakeSystem<ServerComponent>
    {
        public override void Awake(ServerComponent self)
        {
            self.Awake();
        }
    }
}