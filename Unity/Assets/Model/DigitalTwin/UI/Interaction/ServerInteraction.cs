using UnityEngine.EventSystems;
using UnityEngine;

namespace ETModel
{
    public class ServerInteraction: DigitialObjectInteraction
    {
        public Server server;

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            Game.EventSystem.Run(UIEventType.HoverOnServer, this.server);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            Game.EventSystem.Run(UIEventType.CancaleHoverOnServer);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
        }
    }
}