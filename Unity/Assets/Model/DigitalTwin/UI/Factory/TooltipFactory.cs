using System;
using UnityEngine;

namespace ETModel
{
    public class TooltipFactory
    {
        /// <summary>
        /// 加载AssetBundle中对应的类型的资源到对应父物体的UI上
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parent"></param>
        public static UI Create(String type)
        {
            ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
            resourcesComponent.LoadBundle($"{type}.unity3d");
            GameObject prefab = (GameObject)resourcesComponent.GetAsset($"{type}.unity3d", type);
            GameObject tooltip = UnityEngine.Object.Instantiate(prefab);

            UI ui = ComponentFactory.Create<UI, String, GameObject>(type, tooltip);

            return ui;
        }
    }
}