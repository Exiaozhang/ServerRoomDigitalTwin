using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

namespace ETModel
{
    /// <summary>
    /// 此组件用于管理场景中的虚拟摄像头
    /// </summary>
    public class VirtualCameraComponent: Component
    {
        //这里可以优化数据结构可以改成QueueHeap
        private Dictionary<String, CinemachineVirtualCamera> dict = new Dictionary<string, CinemachineVirtualCamera>();

        //当前使用的摄像机
        public CinemachineVirtualCamera Current
        {
            get
            {
                return this.GetMaxPriority();
            }
        }

        public Int32 Count
        {
            get
            {
                return this.dict.Count;
            }
        }

        public void Add(GameObject obj)
        {
            CinemachineVirtualCamera cinemachineVirtualCamera = obj.GetComponent<CinemachineVirtualCamera>();
            if (cinemachineVirtualCamera == null)
                return;
            this.dict.Add(obj.name, cinemachineVirtualCamera);
        }

        public CinemachineVirtualCamera Get(String name)
        {
            CinemachineVirtualCamera result;
            if (!this.dict.TryGetValue(name, out result))
            {
                return null;
            }

            return result;
        }

        /// <summary>
        /// 得到当前优先级最高的虚拟相机
        /// </summary>
        /// <returns></returns>
        public CinemachineVirtualCamera GetMaxPriority()
        {
            if (this.Count == 0)
                return null;

            List<CinemachineVirtualCamera> cinemachineVirtualCameras = this.dict.Values.ToList();
            CinemachineVirtualCamera result = this.dict.Values.First();

            foreach (CinemachineVirtualCamera cinemachineVirtualCamera in cinemachineVirtualCameras)
            {
                if (cinemachineVirtualCamera.Priority >= result.Priority)
                    result = cinemachineVirtualCamera;
            }

            return result;
        }
    }
}