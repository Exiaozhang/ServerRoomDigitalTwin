namespace ETModel
{
    [ObjectSystem]
    public class ServerRoomLobbyComponentSystem: AwakeSystem<ServerRoomLobbyComponent>
    {
        public override void Awake(ServerRoomLobbyComponent self)
        {
            self.Awake();
        }
    }
}