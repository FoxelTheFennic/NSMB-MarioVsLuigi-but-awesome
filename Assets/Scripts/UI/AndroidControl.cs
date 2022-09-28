using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.Android;
using UnityEngine.UI;

public class AndroidControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public bool pressed;

    [SerializeField] GraphicRaycaster m_Raycaster;

    public bool callEveryFrame = false;
    // Declare the delegate (if using non-generic pattern).
    public delegate void onStateChange(bool b);

    // Declare the event.
    public event onStateChange stateChange;

    public void Start()
    {
        stateChange += onChange;
    }
    public void onChange(bool b)
    {
        pressed = b;
    }
    public void Update()
    {
        if (pressed && callEveryFrame)
        {
            stateChange.Invoke(pressed);
        }
        return;

        bool nPressed = false;
        EventSystem.current.useGUILayout = true;
        for(int i = 0; i < Input.touchCount; i++)
        {
            List<RaycastResult> r = new List<RaycastResult>();
            var touch = Input.GetTouch(i);
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = touch.position;
            m_Raycaster.Raycast(eventData, r);
            foreach(RaycastResult result in r)
            {
                if (result.gameObject == this)
                {
                    nPressed = true;
                    Debug.Log("i amg =gssegggbhioseobhigio " + gameObject.name);
                    break;
                }
            }
            if (nPressed) break;
        }
        if (pressed != (pressed = nPressed))
        {
            stateChange.Invoke(nPressed);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        stateChange.Invoke(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        stateChange.Invoke(true);
    }
}
/*
public class AndroidControl : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [HideInInspector]
    public bool pressed;

    public bool callEveryFrame = false;
    public Action<bool> onStateChange;


    public void Start()
    {
        onStateChange = new Action<bool>(onChange);
    }
    public void onChange(bool b)
    {
        pressed = b;
    }

    public void Update()
    {
        if (callEveryFrame && pressed)
            onStateChange.Invoke(true);
    }

    public void OnPointerDown(PointerEventData eventData) { onStateChange.Invoke(true); }
    public void OnPointerUp(PointerEventData eventData)   { onStateChange.Invoke(false); }
}
*/