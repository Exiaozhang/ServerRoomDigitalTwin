using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETModel
{
    public class Room: DigitTwinObject
    {
        public virtual void Awake(GameObject gameObj)
        {
            this.GameObject = UnityEngine.Object.Instantiate(gameObj);
        }
    }
}