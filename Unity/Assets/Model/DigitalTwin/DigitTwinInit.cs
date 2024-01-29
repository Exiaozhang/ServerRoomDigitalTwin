using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Policy;

namespace ETModel
{
    public class DigitTwinInit: MonoBehaviour
    {
        void Start()
        {
            Debug.developerConsoleVisible = true;
            this.StartAsync().Coroutine();
        }

        private async ETVoid StartAsync()
        {
            try
            {
                SynchronizationContext.SetSynchronizationContext(OneThreadSynchronizationContext.Instance);

                DontDestroyOnLoad(gameObject);
                Game.EventSystem.Add(DLLType.Model, typeof (Init).Assembly);

                Game.Scene.AddComponent<TimerComponent>();
                Game.Scene.AddComponent<GlobalConfigComponent>();
                Game.Scene.AddComponent<NetOuterComponent>();
                Game.Scene.AddComponent<ResourcesComponent>();
                Game.Scene.AddComponent<PlayerComponent>();
                Game.Scene.AddComponent<UnitComponent>();
                Game.Scene.AddComponent<UIComponent>();
                Game.Scene.AddComponent<VirtualCameraComponent>();


                // 下载ab包
                await BundleHelper.DownloadBundle();

                //加载Hotfix程序集
                Game.Hotfix.LoadHotfixAssembly();

                //加载配置
                Game.Scene.GetComponent<ResourcesComponent>().LoadBundle("config.unity3d");
                Game.Scene.AddComponent<ConfigComponent>();
                Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle("config.unity3d");
                Game.Scene.AddComponent<OpcodeTypeComponent>();
                Game.Scene.AddComponent<MessageDispatcherComponent>();
                

                Game.Hotfix.GotoHotfix();
                
                //TODO 本项目的UI应该由雷火UX创建

                //创建初始化场景(UI和Map)
                Game.EventSystem.Run(UIEventType.DigitTwinInitSceneStart);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private void Update()
        {
            OneThreadSynchronizationContext.Instance.Update();
            Game.Hotfix.Update?.Invoke();
            Game.EventSystem.Update();
        }

        private void LateUpdate()
        {
            Game.Hotfix.LateUpdate?.Invoke();
            Game.EventSystem.LateUpdate();
        }

        private void OnApplicationQuit()
        {
            Game.Hotfix.OnApplicationQuit?.Invoke();
            Game.Close();
        }
    }
}