using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace ETModel
{
    /// <summary>
    /// 负责提供数字孪生物体的交互效果
    /// </summary>
    public class DigitialObjectInteraction: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public event Action onPointerClikEvent;
        public event Action onPointerEnterEvent;
        public event Action onPointerExitEvent;

        /// <summary>
        /// 高亮Gameobejct控制
        /// </summary>
        [NonSerialized]
        private HighlightableObject HighlightableObject;

        private Boolean rotate = false;

        //旋转速度
        public Single speed = 800;

        //控制是否启用高亮
        [FormerlySerializedAs("Highting")]
        public Boolean HighLighting = false;

        [FormerlySerializedAs("OutLineColor")]
        public Color HighLightingColor = Color.blue;

        public void Awake()
        {
            HighlightableObject hlObj = this.gameObject.GetComponent<HighlightableObject>();

            if (hlObj != null)
            {
                HighlightableObject = hlObj;
                return;
            }

            HighlightableObject = this.gameObject.AddComponent<HighlightableObject>();
        }

        public void ShowOutlineGlow()
        {
            HighlightableObject.ConstantOn(this.HighLightingColor);
        }

        public void OffShowOutlineGlow()
        {
            HighlightableObject.ConstantOff();
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (this.HighLighting)
                this.ShowOutlineGlow();
            onPointerEnterEvent?.Invoke();
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (this.HighLighting)
                this.OffShowOutlineGlow();
            onPointerExitEvent?.Invoke();
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