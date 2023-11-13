using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    GameObject placeholder = MenuManager.Instance.highlightnedPlaceholder;

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        placeholder.SetActive(true);
        placeholder.transform.position = new Vector3(placeholder.transform.position.x, transform.position.y, transform.position.z);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        placeholder.SetActive(false);
    }

}