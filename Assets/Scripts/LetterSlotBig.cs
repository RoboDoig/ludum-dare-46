using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LetterSlotBig : MonoBehaviour, IDropHandler
{
    public GameManager gameManager;
    public int sortOrder;
    public bool locked = false;

    private LetterSlotDisplay slotDisplay;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && !locked)
        {
            slotDisplay = eventData.pointerDrag.GetComponent<LetterSlotDisplay>();
            if (slotDisplay != null)
            {
                SetText(slotDisplay.GetComponentInChildren<Text>().text);
                Destroy(slotDisplay.gameObject);
                Destroy(slotDisplay.parentLayoutTransform.gameObject);
            }
        }
    }

    public void SetText(string str)
    {
        GetComponentInChildren<Text>().text = str;
    }

    public void RemoveLetter()
    {

    }
}
