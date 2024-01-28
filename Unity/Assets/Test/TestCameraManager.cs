using System;
using Cinemachine;
using UnityEngine;

namespace Test
{
    public class TestCameraManager: MonoBehaviour
    {
        public static TestCameraManager Instance;
        public CinemachineVirtualCamera cinemachineVirtualCamera;

        public void Awake()
        {
            Instance = this;
        }

        public void FocusObject(GameObject focused)
        {
            this.cinemachineVirtualCamera.Follow = focused.transform;
            this.cinemachineVirtualCamera.LookAt = focused.transform;
        }
    }
}