using Cinemachine;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class VirtualCameraComponentSystem: AwakeSystem<VirtualCameraComponent>
    {
        public override void Awake(VirtualCameraComponent self)
        {
            self.Add(Component.Global.transform.Find("CM vcam1").gameObject);
        }
    }
}