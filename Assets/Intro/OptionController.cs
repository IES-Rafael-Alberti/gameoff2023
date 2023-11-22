using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{



    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.GetComponent<Button>() && !gameObject.GetComponent<Button>().interactable) return;
        GameObject placeholder = MenuManager.Instance.highlightnedPlaceholder;
        placeholder.SetActive(true);
        placeholder.transform.position = new Vector3(placeholder.transform.position.x, transform.position.y, transform.position.z);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MenuManager.Instance.highlightnedPlaceholder.SetActive(false);
    }

}