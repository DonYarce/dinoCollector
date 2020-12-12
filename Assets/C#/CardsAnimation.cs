using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardsAnimation : MonoBehaviour, IPointerDownHandler
{
    public RectTransform rt;
    [TextArea(15, 20)]public string txt;
    public Image img;
    public Camera cam;

    

    public void OnPointerDown(PointerEventData eventData)
    {
        if(CardsController.instance.isSelected == false)
        {
            CardsController.instance.isSelected = true;
            StopAllCoroutines();
            StartCoroutine(CardsController.instance.selectCard(this.gameObject));
        }
        else
        {
            CardsController.instance.isSelected = false;
            StopAllCoroutines();
            StartCoroutine(CardsController.instance.unselectCard(this.gameObject));
        }
        
    }
}
