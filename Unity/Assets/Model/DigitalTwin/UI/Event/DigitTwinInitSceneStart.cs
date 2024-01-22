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
            //加载资源中的Room的预制体
            GameObject roomObj = Resources.Load<GameObject>("DigitTwin/Room");
            ServerRoom serverRoom = ComponentFactory.CreateWithParent<ServerRoom, GameObject>(Game.Scene, roomObj);

            //接收服务器传来的ServerRack配置信息(这里先用AssetBundleConfig代替 还没有清楚需求)
            ACategory serverRackConfigCategory = Game.Scene.GetComponent<ConfigComponent>().GetCategory(typeof (ServerRackConfig));
            IConfig[] serverRackConfigs = serverRackConfigCategory.GetAll();

            foreach (var config in serverRackConfigs)
            {
                var serverRackConfig = (ServerRackConfig)config;
                Game.EventSystem.Run(EventType.AddServerRack, serverRackConfig.Id, serverRackConfig.Position);
            }

            //接收服务器传来的Server配置信息(这里先用AssetBundleConfig代替)
            ACategory serverConfigCategory = Game.Scene.GetComponent<ConfigComponent>().GetCategory(typeof (ServerConfig));
            IConfig[] serverConfigs = serverConfigCategory.GetAll();

            foreach (IConfig config in serverConfigs)
            {
                var serverConfig = (ServerConfig)config;
                Game.EventSystem.Run(EventType.AddServer, serverConfig.Id, serverConfig.RackId, serverConfig.Position);
            }
        }
    }
}