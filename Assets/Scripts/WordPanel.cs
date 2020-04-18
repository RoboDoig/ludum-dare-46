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
        CharacterPanel[] characterPanels = gameObject.GetComponentsInChildren<CharacterPanel>();

        foreach(CharacterPanel charPanel in characterPanels)
        {
            string character = charPanel.GetCharacter();
            if (character != "")
            {
                word += character;
            }
        }
        return word.ToLower();
    }
}
