using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetWord()
    {
        string word = "";
        LetterSlotBig[] letterPanels = gameObject.GetComponentsInChildren<LetterSlotBig>();

        foreach(LetterSlotBig letterPanel in letterPanels)
        {
            string character = letterPanel.GetCharacter();
            if (character != "")
            {
                word += character;
            }
        }
        return word.ToLower();
    }
}
