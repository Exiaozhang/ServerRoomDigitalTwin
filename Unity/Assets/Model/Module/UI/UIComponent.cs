using System;
using System.Collections.Generic;
using ETHotfix;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class UIComponentAwakeSystem: AwakeSystem<UIComponent>
    {
        public override void Awake(UIComponent self)
        {
            self.Root = Component.Global.transform.Find("UICamera").gameObject;
        }
    }

    /// <summary>
    /// 管理所有UI
    /// </summary>
    public class UIComponent: Component
    {
        public GameObject Root;

        /// <summary>
        /// 存储所有UI
        /// </summary>
        public Dictionary<string, UI> uis = new Dictionary<string, UI>();

        /// <summary>
        /// 存储所有UI的工厂，用于创建UI
        /// </summary>
        public Dictionary<string, IUIFactory> UiTypes = new Dictionary<string, IUIFactory>();

        public void Add(UI ui)
        {
            ui.GameObject.GetComponent<Canvas>().worldCamera = this.Root.GetComponent<Camera>();
            this.uis.Add(ui.Name, ui);
            ui.Parent = this;
        }

        public void Remove(string name)
        {
            if (!this.uis.TryGetValue(name, out UI ui))
            {
                return;
            }

            this.uis.Remove(name);
            ui.Dispose();
        }

        public UI Get(string name)
        {
            UI ui = null;
            this.uis.TryGetValue(name, out ui);
            return ui;
        }

        public UI Create(String type)
        {
            try
            {
                UI ui = this.UiTypes[type].Create(this.GetParent<Scene>(), type, this.Root);
                this.uis.Add(type, ui);

                //设置canvas
                string cavasName = ui.GameObject.GetComponent<CanvasConfig>().CanvasName;
                ui.GameObject.transform.SetParent(this.Root.Get<GameObject>(cavasName).transform, false);

                return ui;
            }
            catch (Exception e)
            {
                throw new Exception($"{type} UI 错误: {e}");
            }
        }
    }
}