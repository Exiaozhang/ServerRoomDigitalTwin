using System;
using UnityEngine;

namespace ETModel
{
    public class ServerRackLobbyFactory
    {
        public static UI Create(String type)
        {
            //加载AssestBundle中的资源信息,实例化出Object
            Game.Scene.GetComponent<ResourcesComponent>().LoadBundle($"{type}.unity3d");
            GameObject prefab = (GameObject)Game.Scene.GetComponent<ResourcesComponent>().GetAsset($"{type}.unity3d", type);
            GameObject ServerRackLobby = UnityEngine.Object.Instantiate(prefab);

            UI ui = ComponentFactory.Create<UI, String, GameObject>(type, ServerRackLobby);
            ui.AddComponent<ServerRackLobbyComponent>();
            
            return ui;
        }
    }
}