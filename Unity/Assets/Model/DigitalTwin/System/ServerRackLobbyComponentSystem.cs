using UnityEngine;
using UnityEngine.UI;

namespace ETModel
{
    [ObjectSystem]
    public class ServerRackLobbyComponentSystem: AwakeSystem<ServerRackLobbyComponent>
    {
        public override void Awake(ServerRackLobbyComponent self)
        {
            ReferenceCollector referenceCollector = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            referenceCollector.Get<GameObject>("BackToLobbyButton").GetComponent<Button>().onClick.Add(() =>
            {
                //清理这个场景的UI
                UI ui = Game.Scene.GetComponent<UIComponent>().Get(DigitialTwinUIType.ServerRackLobby);
                ui.Dispose();

                //切换到机房场景
                Game.EventSystem.Run(UIEventType.SwitchToServerRoomScene);
            });
        }
    }
}