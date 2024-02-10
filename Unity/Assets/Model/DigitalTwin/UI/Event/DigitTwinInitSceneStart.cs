using Cinemachine;
using UnityEngine;

namespace ETModel
{
    /// <summary>
    /// 初始化场景
    /// </summary>
    [Event(UIEventType.DigitTwinInitSceneStart)]
    public class DigitTwinInitSceneStart: AEvent
    {
        public override async void Run()
        {
            //创建加载主场景大厅UI
            UI ui = LobbyFactory.Create(DigitialTwinUIType.MainLobby);
            //将UI加载到Scene中的UIComponent进行管理
            Game.Scene.GetComponent<UIComponent>().Add(ui);

            //切换到机房的场景
            Game.EventSystem.Run(UIEventType.SwitchToServerRoomScene);
        }
    }
}