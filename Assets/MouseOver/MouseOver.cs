using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Outline outline;

    void Start()
    {
        outline.enabled = false;
    }

    void IPointerEnterHandler.OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        outline.enabled = true;
    }

    void IPointerExitHandler.OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        outline.enabled = false;
    }

    public void setOutline(Outline outline)
    {
        this.outline = outline;
    }
}
