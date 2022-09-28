using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.Android;
using UnityEngine.UI;

public class AndroidControlStay : AndroidControl, IPointerUpHandler, IPointerDownHandler
{
    // Declare the delegate (if using non-generic pattern).
    public new delegate void onStateChange(bool b);

    // Declare the event.
    public new event onStateChange stateChange;

    public new void OnPointerExit(PointerEventData eventData) {}

    public new void OnPointerEnter(PointerEventData eventData) {}

    public new void Start()
    {
        stateChange += onChange;
    }
    public new void onChange(bool b)
    {
        pressed = b;
    }

    public new void Update()
    {
        if (callEveryFrame && pressed)
            stateChange.Invoke(true);
    }

    public void OnPointerDown(PointerEventData eventData) { stateChange.Invoke(true); }
    public void OnPointerUp(PointerEventData eventData) { stateChange.Invoke(false); }
}