using Cinemachine;
using UnityEngine;
using EventType = ETModel.Event.EventType;

namespace ETModel
{
    /// <summary>
    /// 初始化场景
    /// </summary>
    [Event(UIEventType.DigitTwinInitSceneStart)]
    public class DigitTwinInitSceneStart: AEvent
    {
        public override void Run()
        {
            //创建加载主场景大厅UI
            UI ui = LobbyFactory.Create(DigitialTwinUIType.Lobby);
            //将UI加载到Scene中的UIComponent进行管理
            Game.Scene.GetComponent<UIComponent>().Add(ui);

            //加载资源中的Room的预制体
            GameObject roomObj = Resources.Load<GameObject>("DigitTwin/Room");
            ServerRoom serverRoom = ComponentFactory.CreateWithParent<ServerRoom, GameObject>(Game.Scene, roomObj);

            //接收服务器传来的ServerRack配置信息(这里先用AssetBundleConfig代替 还没有清楚需求)
            ACategory serverRackConfigCategory = Game.Scene.GetComponent<ConfigComponent>().GetCategory(typeof (ServerRackConfig));
            IConfig[] serverRackConfigs = serverRackConfigCategory.GetAll();

            foreach (var config in serverRackConfigs)
            {
                var serverRackConfig = (ServerRackConfig)config;
                //发布事件添加机架
                Game.EventSystem.Run(EventType.AddServerRack, serverRackConfig);
            }

            //接收服务器传来的Server配置信息(这里先用AssetBundleConfig代替)
            ACategory serverConfigCategory = Game.Scene.GetComponent<ConfigComponent>().GetCategory(typeof (ServerConfig));
            IConfig[] serverConfigs = serverConfigCategory.GetAll();

            foreach (IConfig config in serverConfigs)
            {
                var serverConfig = (ServerConfig)config;
                Game.EventSystem.Run(EventType.AddServer, serverConfig);
            }

            //将虚拟摄像头对准ServerRoom
            CinemachineVirtualCamera cinemachineVirtualCamera = Game.Scene.GetComponent<VirtualCameraComponent>().GetMaxPriority();
            cinemachineVirtualCamera.LookAt = serverRoom.GameObject.transform;
            cinemachineVirtualCamera.Follow = serverRoom.GameObject.transform;
            UnityEngine.Object.Destroy(cinemachineVirtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body));
            UnityEngine.Object.Destroy(cinemachineVirtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Aim));

            cinemachineVirtualCamera.AddCinemachineComponent<CinemachineComposer>();
            cinemachineVirtualCamera.transform.position = serverRoom.GameObject.transform.position + new Vector3(0, 5, -15);

            serverRoom.Interaction.RotateControl(true);
        }
    }
}