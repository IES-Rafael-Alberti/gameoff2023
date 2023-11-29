using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveIndicator : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    public GameObject Indicator;

    // Called when the mouse pointer enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        Indicator.SetActive(true);
        Indicator.transform.position = transform.position;
    }
    public void OnSelect(BaseEventData eventData)
    {
        Indicator.SetActive(true);
        Indicator.transform.position = transform.position;
    }

}
