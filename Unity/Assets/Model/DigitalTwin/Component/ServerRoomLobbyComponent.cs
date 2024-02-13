using UnityEngine;
using UnityEngine.UI;

namespace ETModel
{
    /// <summary>
    /// 初始化UI
    /// </summary>
    public class ServerRoomLobbyComponent: Component
    {
        public void Awake()
        {
            ReferenceCollector referenceCollector = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            referenceCollector.Get<GameObject>("TemButton").GetComponent<Button>().onClick.Add(() =>
            {
                UnityEngine.MonoBehaviour.print("显示/关闭温度场");
                Game.EventSystem.Run(UIEventType.SiwtchHeatMap);
            });
        }
    }
}