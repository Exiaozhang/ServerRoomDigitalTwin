using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETModel.Event
{
    /// <summary>
    /// 添加机架到ServerRoom中对应的位置
    /// Para Int32:机架的ID Int32:机架的Position 
    /// </summary>
    [Event(EventType.AddServerRack)]
    public class AddServerRack: AEvent<ServerRackConfig>
    {
        public override async void Run(ServerRackConfig serverRackConfig)
        {
            //向机房中添加服务器
            ServerRack serverRack = ServerRoom.Instance.AddServerRack(serverRackConfig);

            //把温度写入到heatmap中的配置文件中
            //这块的Heatmap有问题，只会叠加时间的数量，无法配置温度
            //暂时的解决方法就是如果温度高则多次叠加
            IEventWriter eventWriter = new JSONEventWriter("Assets/Test-data/json-test123.txt", true);
            if (eventWriter.IsWriterAvailable())
            {
                eventWriter.SaveEvent(new BaseEvent("ServerRackTem", serverRack.GameObject.transform.position));
            }

            //给机架订阅事件，点击切换场景
            serverRack.Interaction.onPointerClikEvent += () => { Game.EventSystem.Run(UIEventType.SwitchToServerRackScene, serverRack); };

            serverRack.Interaction.HighLighting = true;
        }
    }
}