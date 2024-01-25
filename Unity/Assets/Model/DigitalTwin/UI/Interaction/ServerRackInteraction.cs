using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace ETModel
{
    public class ServerRackInteraction: DigitialObjectInteraction
    {
        public ServerRack serverRack;

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            ServerComponent serverComponent = this.serverRack.GetComponent<ServerComponent>();
            var servers = serverComponent.GetAll();

            List<ServerConfig> serverConfigs = new List<ServerConfig>();
            foreach (Server server in servers)
            {
                serverConfigs.Add(new ServerConfig() { Id = server.Id, Name = "", Position = server.Position });
            }

            Game.EventSystem.Run(UIEventType.SwitchServrRackScene, this.serverRack);
        }
    }
}