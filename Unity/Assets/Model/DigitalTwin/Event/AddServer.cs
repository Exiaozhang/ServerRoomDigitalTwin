using System;

namespace ETModel.Event
{
    [Event(EventType.AddServer)]
    public class AddServer: AEvent<Int32, Int32, Int32>
    {
        public override void Run(int id, int rackId, int position)
        {
            ServerRackComponent serverRackComponent = ServerRoom.Instance.GetComponent<ServerRackComponent>();
            ServerRack serverRack = serverRackComponent.GetServerRack(rackId);
            if (serverRack == null)
            {
                return;
            }

            ServerComponent serverComponent = serverRack.GetComponent<ServerComponent>();
            if (serverComponent == null)
                serverComponent = serverRack.AddComponent<ServerComponent>();

            Server addServer = serverComponent.AddServer(id, position);
            serverRack.AddServer(addServer.GameObject, addServer.Position);
        }
    }
}