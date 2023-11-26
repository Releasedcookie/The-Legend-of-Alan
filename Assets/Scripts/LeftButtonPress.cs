using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LeftButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool ButtonPressed = false;

    // Update is called once per frame
    void Update()
    {
        if (ButtonPressed)
        {
            FindFirstObjectByType<PlayerMovement>().ButtonControl_moveLeft();
        }
        else
        {
            FindFirstObjectByType<PlayerMovement>().ButtonControl_StopLeft();
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        ButtonPressed = true;
    }
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        ButtonPressed = false;
    }

}
