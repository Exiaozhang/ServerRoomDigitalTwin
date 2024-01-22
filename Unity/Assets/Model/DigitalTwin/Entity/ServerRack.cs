using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETModel
{
    /// <summary>
    /// 机架,机架由上面的服务器和交换机组成等等
    /// </summary>
    public class ServerRack: DigitTwinObject
    {
        /// <summary>
        /// 机架温度
        /// </summary>
        /// <value></value>
        public Single Temperature { get; set; }

        public Int32 Id { get; set; }

        public Int32 Position { get; set; }

        public void Awake(GameObject gameObj, Int32 id, Int32 Position)
        {
            Temperature = 0;
            this.GameObject = UnityEngine.Object.Instantiate(gameObj);
            this.Id = id;
            this.Position = Position;
        }

        /// <summary>
        /// 在场景中将Server的Gameobject添加到ServerRack上
        /// </summary>
        public void AddServer(GameObject gameObject, Int32 position)
        {
            ReferenceCollector referenceCollector = this.GameObject.GetComponent<ReferenceCollector>();
            Transform transform = referenceCollector.Get<GameObject>(position.ToString()).GetComponent<Transform>();
            gameObject.transform.position = transform.position;
            gameObject.transform.rotation = transform.rotation;
            gameObject.transform.parent = this.GameObject.transform;
        }
    }
}