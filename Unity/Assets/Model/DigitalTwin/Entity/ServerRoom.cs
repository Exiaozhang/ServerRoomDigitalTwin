using System;
using UnityEngine;

namespace ETModel
{
    /// <summary>
    /// 机房由机架组成
    /// </summary>
    public class ServerRoom: Room
    {
        public static ServerRoom Instance { get; protected set; }

        public override void Awake(GameObject gameObj)
        {
            base.Awake(gameObj);
            this.AddComponent<ServerRackComponent>();
            Instance = this;
        }

        /// <summary>
        /// 将Gameobject放到ServeRoom中的具体位置
        /// </summary>
        /// <param name="obj">Gameobject</param>
        /// <param name="position">位置</param>
        public void Add(GameObject obj, Int32 position)
        {
            
            if (obj == null)
                return;
            ReferenceCollector referenceCollector = this.GameObject.GetComponent<ReferenceCollector>();
            GameObject gameObject = referenceCollector.Get<GameObject>(position.ToString());
            obj.transform.position = gameObject.transform.position;
            obj.transform.rotation = gameObject.transform.rotation;
            obj.GetComponent<Transform>().parent = this.GameObject.transform;
        }
    }
}