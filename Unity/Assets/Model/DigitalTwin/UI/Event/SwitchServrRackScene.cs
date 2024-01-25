using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETModel
{
    [Event(UIEventType.SwitchServrRackScene)]
    public class SwitchServrRackScene: AEvent<ServerRack>
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
                newServerRack.AddServer(new ServerConfig()
                {
                    Id = server.Id, Name = server.Name, Position = server.Position, RackId = server.RackId
                });
            }

            serverRack.Dispose();
        }
    }
}