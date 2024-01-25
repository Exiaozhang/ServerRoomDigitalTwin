using System;

namespace ETModel.Event
{
    [Event(EventType.AddServer)]
    public class AddServer: AEvent<ServerConfig>
    {
        public override void Run(ServerConfig serverConfig)
        {
            ServerRackComponent serverRackComponent = ServerRoom.Instance.GetComponent<ServerRackComponent>();
            ServerRack serverRack = serverRackComponent.GetServerRack(serverConfig.RackId);
            if (serverRack == null)
            {
                return;
            }

            //添加服务器到机架上
            serverRack.AddServer(serverConfig);
        }
    }
}