﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvailableLetterPanel : MonoBehaviour
{
    public GameObject letterSlot;
    public GameObject letterSlotDisplay;

    public List<GameObject> letterSlots = new List<GameObject>();
    public List<LetterSlotDisplay> letterDisplays = new List<LetterSlotDisplay>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitialiseLetters(char[] characters)
    {
        foreach (char character in characters)
        {
            // create a new letter slot for each character
            GameObject newSlot = Instantiate(letterSlot);
            newSlot.transform.SetParent(transform);
            letterSlots.Add(newSlot);

            // create letter display for each character
            GameObject newDisplay = Instantiate(letterSlotDisplay);
            letterDisplays.Add(newDisplay.GetComponent<LetterSlotDisplay>());
            newDisplay.transform.SetParent(transform);
            newDisplay.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-1000,1000), 1000);
            newDisplay.GetComponent<LayoutElement>().ignoreLayout = true;
            newDisplay.GetComponent<LetterSlotDisplay>().parentLayoutTransform = newSlot.GetComponent<RectTransform>();
            newDisplay.GetComponentInChildren<Text>().text = character.ToString().ToUpper();
        }
    }

    public void ClearLetters()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        letterSlots = new List<GameObject>();
        letterDisplays = new List<LetterSlotDisplay>();
    }

    public void KeepLetters()
    {
        foreach (LetterSlotDisplay slotDisplay in letterDisplays)
        {
            slotDisplay.GetComponent<Image>().color = Color.red;
            if (slotDisplay.available)
            {
                slotDisplay.kept = true;
            }
        }
    }

    public void UnKeepLetters()
    {
        foreach (LetterSlotDisplay slotDisplay in letterDisplays)
        {
            slotDisplay.GetComponent<Image>().color = Color.red;
            slotDisplay.kept = false;
        }
    }

    public int CountKeptLetters()
    {
        int keptLetters = 0;
        foreach (LetterSlotDisplay slotDisplay in letterDisplays)
        {
            if (slotDisplay.kept)
            {
                keptLetters++;
            }
        }
        return keptLetters;
    }
}
