using System;
using UnityEngine;

namespace ETModel
{
    /// <summary>
    /// 机房由机架组成
    /// </summary>
    public class ServerRoom: Room
    {
        public static ServerRoom Instance { get; protected set; }

        public ServerRoomInteraction Interaction { get; protected set; }

        public Boolean IsOpenHeatMap { get; protected set; }

        public override void Awake(GameObject gameObj)
        {
            base.Awake(gameObj);
            this.AddComponent<ServerRackComponent>();
            Instance = this;
            this.Interaction = this.GameObject.AddComponent<ServerRoomInteraction>();
            this.Interaction.speed = 800;
        }

        /// <summary>
        /// 将Gameobject放到ServeRoom中的具体位置
        /// </summary>
        /// <param name="obj">Gameobject</param>
        /// <param name="position">位置</param>
        private void SetPosition(GameObject obj, Int32 position)
        {
            if (obj == null)
                return;
            ReferenceCollector referenceCollector = this.GameObject.GetComponent<ReferenceCollector>();
            GameObject gameObject = referenceCollector.Get<GameObject>(position.ToString());
            obj.transform.position = gameObject.transform.position;
            obj.transform.rotation = gameObject.transform.rotation;
            obj.GetComponent<Transform>().parent = this.GameObject.transform;
        }

        /// <summary>
        /// 向机房中添加机架
        /// </summary>
        /// <param name="serverRackConfig"></param>
        /// <returns></returns>
        public ServerRack AddServerRack(ServerRackConfig serverRackConfig)
        {
            GameObject serverRackObj = Resources.Load<GameObject>("DigitTwin/ServerRack");
            ServerRack serverRack =
                    ComponentFactory.CreateWithParent<ServerRack, GameObject, ServerRackConfig>(this, serverRackObj, serverRackConfig);
            this.GetComponent<ServerRackComponent>().AddServerRack(serverRack.Id, serverRack);
            this.SetPosition(serverRack.GameObject, serverRack.Position);
            return serverRack;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        /// <summary>
        /// 控制温度场的开关
        /// </summary>
        public void SiwtchHeatMap()
        {
            HeatmapController heatmapController = this.GameObject.GetComponent<HeatmapController>();
            if (!this.IsOpenHeatMap)
            {
                heatmapController.LoadEvents();
                foreach (EventData eventData in heatmapController.events)
                {
                    eventData.ShouldEventBeVisualised = true;
                }

                heatmapController.InitializeParticleSystem();
                heatmapController.AddSelectedEventsToHeatmap();
                this.IsOpenHeatMap = true;
                return;
            }
            heatmapController.CloseParticleSystem();
            this.IsOpenHeatMap = false;
        }
    }
}