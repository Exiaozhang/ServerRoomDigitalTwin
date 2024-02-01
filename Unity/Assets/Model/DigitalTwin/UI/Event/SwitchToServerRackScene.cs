using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace ETModel
{
    [Event(UIEventType.SwitchToServerRackScene)]
    public class SwitchToServerRackScene: AEvent<ServerRack>
    {
        public override async void Run(ServerRack serverRack)
        {
            // 加载场景资源
            // 加载场景会清空Gameobject
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().LoadBundle("serverrackscene.unity3d");

            using (SceneChangeComponent sceneChangeComponent = ETModel.Game.Scene.AddComponent<SceneChangeComponent>())
            {
                await sceneChangeComponent.ChangeSceneAsync(SceneType.ServerRackScene);
            }

            //创建加载ServerRack
            //加载AesstBundle种的预制体
            GameObject serverRackObj = Resources.Load<GameObject>("DigitTwin/ServerRack");
            ServerRackConfig serverRackConfig = new ServerRackConfig() { Name = serverRack.Name, Position = serverRack.Position, Id = serverRack.Id };
            ServerRack newServerRack =
                    ComponentFactory.CreateWithParent<ServerRack, GameObject, ServerRackConfig>(Game.Scene, serverRackObj, serverRackConfig);

            ServerComponent serverComponent = serverRack.GetComponent<ServerComponent>();
            List<Server> servers = serverComponent.GetAll();

            foreach (Server server in servers)
            {
                Server addServer = newServerRack.AddServer(new ServerConfig()
                {
                    Id = server.Id, Name = server.Name, Position = server.Position, RackId = server.RackId
                });
                addServer.Interaction.HighLighting = true;
            }

            serverRack.Dispose();

            //切换游戏摄像机视角
            CinemachineVirtualCamera cinemachineVirtualCamera = Game.Scene.GetComponent<VirtualCameraComponent>().Current;

            //Follow和LookAt ServerRack
            cinemachineVirtualCamera.Follow = newServerRack.GameObject.transform;
            cinemachineVirtualCamera.LookAt = newServerRack.GameObject.transform;

            cinemachineVirtualCamera.transform.position = newServerRack.GameObject.transform.position + new Vector3(0, 0, -5);

            newServerRack.Interaction.RotateControl(true);

            //加载机架场景中的UI
            UI ui = ServerRackLobbyFactory.Create(DigitialTwinUIType.ServerRackLobby);
            Game.Scene.GetComponent<UIComponent>().Add(ui);
        }
    }
}