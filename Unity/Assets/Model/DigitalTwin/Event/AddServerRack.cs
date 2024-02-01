using System;
using System.Collections.Generic;

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
            //向机房中添加服务器
            ServerRack serverRack = ServerRoom.Instance.AddServerRack(serverRackConfig);

            //给机架订阅事件，点击切换场景
            serverRack.Interaction.onPointerClikEvent += () => { Game.EventSystem.Run(UIEventType.SwitchToServerRackScene, serverRack); };
            serverRack.Interaction.HighLighting = true;
        }
    }
}