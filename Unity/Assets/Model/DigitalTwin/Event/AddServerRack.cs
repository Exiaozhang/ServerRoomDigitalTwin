using System;

namespace ETModel.Event
{
    /// <summary>
    /// 添加机架到ServerRoom中对应的位置
    /// Para Int32:机架的ID Int32:机架的Position 
    /// </summary>
    [Event(EventType.AddServerRack)]
    public class AddServerRack: AEvent<ServerRackConfig>
    {
        public override void Run(ServerRackConfig serverRackConfig)
        {
            ServerRackComponent serverRackComponent = ServerRoom.Instance.GetComponent<ServerRackComponent>();
            ServerRack serverRack = serverRackComponent.AddServerRack(serverRackConfig);
            ServerRoom.Instance.Add(serverRack.GameObject, serverRack.Position);

            //给ServerRack添加交互组件
            ServerRackInteraction serverRackInteraction = serverRack.GameObject.AddComponent<ServerRackInteraction>();
            serverRackInteraction.serverRack = serverRack;
        }
    }
}