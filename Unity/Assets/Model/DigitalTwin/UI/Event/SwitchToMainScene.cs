using Cinemachine;
using UnityEngine;

namespace ETModel
{
    [Event(UIEventType.SwitchToMainScne)]
    public class SwitchToMainScene: AEvent
    {
        public override async void Run()
        {
            // 加载场景资源
            // 加载场景会清空Gameobject
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().LoadBundle("digittwininit.unity3d");

            using (SceneChangeComponent sceneChangeComponent = ETModel.Game.Scene.AddComponent<SceneChangeComponent>())
            {
                await sceneChangeComponent.ChangeSceneAsync(SceneType.DigitTwinInit);
            }

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