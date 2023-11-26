using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool ButtonPressed = false;

    // Update is called once per frame
    void Update()
    {
        if (ButtonPressed)
        {
            FindFirstObjectByType<PlayerMovement>().ButtonControl_jump();
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