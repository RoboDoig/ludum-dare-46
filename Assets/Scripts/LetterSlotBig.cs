using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LetterSlotBig : MonoBehaviour, IDropHandler, IPointerDownHandler
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
                slotDisplay.DropSuccess();
                locked = true;
            }
        }
    }

    public void SetText(string str)
    {
        GetComponentInChildren<Text>().text = str;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (slotDisplay != null)
        {
            slotDisplay.Undrop();
            locked = false;
            SetText("");
        }
    }

    public string GetCharacter()
    {
        return GetComponentInChildren<Text>().text;
    }
}
