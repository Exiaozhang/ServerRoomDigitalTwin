using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ETModel
{
    /// <summary>
    /// 负责提供数字孪生物体的交互效果
    /// </summary>
    public class DigitialObjectInteraction: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public event Action onPointerClikEvent;
        private Boolean rotate = false;
        
        //旋转速度
        public Single speed = 800;

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
            this.onPointerClikEvent?.Invoke();
        }

        /// <summary>
        /// 控制鼠标是否可以控制ServeRoom的旋转
        /// </summary>
        /// <param name="Rotate"></param>
        public void RotateControl(Boolean Rotate)
        {
            rotate = Rotate;
        }

        public void Update()
        {
            if (Input.GetMouseButton(0) && this.rotate)
            {
                float OffsetX = Input.GetAxis("Mouse X") * this.speed * Time.deltaTime;

                transform.Rotate(new Vector3(0, -OffsetX, 0), Space.World);
            }
        }
    }
}