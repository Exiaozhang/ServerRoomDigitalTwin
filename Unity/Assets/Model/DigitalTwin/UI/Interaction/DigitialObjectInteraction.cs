using UnityEngine;
using UnityEngine.EventSystems;

namespace ETModel
{
    /// <summary>
    /// 负责提供数字孪生物体的交互效果
    /// </summary>
    public class DigitialObjectInteraction: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public void ShowOutlineGlow()
        {
            //TODO 完成外发光效果
            Log.Msg("外发光");
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            this.ShowOutlineGlow();
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
        }
    }
}