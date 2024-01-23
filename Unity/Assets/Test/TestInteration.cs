using System.Collections;
using System.Collections.Generic;
using ETModel;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestInteration: MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        print("Hello World");
    }
}