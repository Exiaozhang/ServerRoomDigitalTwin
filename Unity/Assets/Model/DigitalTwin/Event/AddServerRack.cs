using System;

namespace ETModel.Event
{
    /// <summary>
    /// 添加机架到ServerRoom中对应的位置
    /// Para Int32:机架的ID Int32:机架的Position 
    /// </summary>
    [Event(EventType.AddServerRack)]
    public class AddServerRack: AEvent<Int32, Int32>
    {
        public override void Run(int id, int position)
        {
            ServerRackComponent serverRackComponent = ServerRoom.Instance.GetComponent<ServerRackComponent>();
            ServerRack serverRack = serverRackComponent.AddServerRack(id, position);
            ServerRoom.Instance.Add(serverRack.GameObject, serverRack.Position);
        }
    }
}