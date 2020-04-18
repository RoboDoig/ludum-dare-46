using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WordPanel wordPanel;
    public AvailableLetterPanel availableLetterPanel;
    public GameObject letterSlot;

    public TextAsset wordsAsset;
    string[] wordArray;
    List<string> wordList;
    List<string> previousWorldList;
    string letterChoice;

    [Header("Game Parameters")]
    public int startingLives = 10;
    public int minNewLettersPerRound = 1;
    public int maxNewLettersPerRound = 4;

    int gameRounds = 0;
    int currentLives;

    // Start is called before the first frame update
    void Awake()
    {
        char[] archDelim = new char[] { '\r', '\n' };
        wordArray = wordsAsset.text.Split(archDelim);
        wordList = new List<string>(wordArray);

        previousWorldList = new List<string>();

        letterChoice = "abcdefghijklmnopqrstuvwxyz";
    }

    void Start()
    {
        // Begin new round
        // how many letters do we get this round?
        int newLettersThisRound = Random.Range(minNewLettersPerRound, maxNewLettersPerRound);

        // choose those letters
        char[] newLetters = new char[newLettersThisRound];
        for (int i = 0; i < newLettersThisRound; i++)
        {
            newLetters[i] = letterChoice[Random.Range(0, letterChoice.Length)];
        }

        // show them in the UI
        availableLetterPanel.InitialiseLetters(newLetters);

        // create initial word panel - IT
        SetWordPanel(" IT ");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AliveButtonClicked()
    {
        // Get the current word
        string word = wordPanel.GetWord();
        
        // Scoring logic

        // Progress to next round
    }

    public bool CheckWord(string word)
    {
        if (wordList.Contains(word))
        {
            return true;
        } else
        {
            return false;
        }
    }

    void SetWordPanel(string str)
    {
        int i = 0;
        foreach (char c in str)
        {
            GameObject slot = Instantiate(letterSlot);
            slot.transform.SetParent(wordPanel.transform);

            LetterSlotBig slotComponent = slot.GetComponent<LetterSlotBig>();
            slotComponent.SetText(c.ToString());
            slotComponent.sortOrder = i;
            slotComponent.gameManager = this;

            i++;
        }
    }
}
