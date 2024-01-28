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
                Game.EventSystem.Run(UIEventType.ServerRackSceneLobbyToLobby);
            });
        }
    }
}