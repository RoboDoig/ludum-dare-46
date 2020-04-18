using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvailableLetterPanel : MonoBehaviour
{
    public GameObject letterSlot;
    public GameObject letterSlotDisplay;

    public List<GameObject> letterSlots = new List<GameObject>();

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
            newDisplay.transform.SetParent(transform);
            newDisplay.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-1000,1000), 1000);
            newDisplay.GetComponent<LayoutElement>().ignoreLayout = true;
            newDisplay.GetComponent<LetterSlotDisplay>().parentLayoutTransform = newSlot.GetComponent<RectTransform>();
            newDisplay.GetComponentInChildren<Text>().text = character.ToString().ToUpper();
        }
    }
}
