﻿using UnityEngine;

namespace ETModel
{
    /// <summary>
    /// 控制温度场的开关
    /// </summary>
    [Event(UIEventType.SiwtchHeatMap)]
    public class SwitchHeatMap: AEvent
    {
        public override void Run()
        {
            //由于bug生成的温度场不会跟物体旋转，只好调整到固定角度和关闭控制
            ServerRoom.Instance.Interaction.RotateControl(ServerRoom.Instance.IsOpenHeatMap);
            ServerRoom.Instance.GameObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
            
            Session session = Game.Scene.GetComponent<NetOuterComponent>().Create(GlobalConfigComponent.Instance.GlobalProto.Address); 
            session.Call(new C2G_TestMessage(){Message = "Hello World"});
            
            ServerRoom.Instance.SiwtchHeatMap();
        }
    }
}