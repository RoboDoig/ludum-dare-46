using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public WordPanel wordPanel;
    public AvailableLetterPanel availableLetterPanel;
    public GameObject letterSlot;
    public Text roundsText;
    public Text livesText;

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
    int score = 0;
    int currentLives;
    bool keepToggle = false;

    // Start is called before the first frame update
    void Awake()
    {
        char[] archDelim = new char[] { '\r', '\n' };
        wordArray = wordsAsset.text.Split(archDelim);
        wordList = new List<string>(wordArray);

        previousWorldList = new List<string>();
        previousWorldList.Add("it");

        letterChoice = "abcdefghijklmnopqrstuvwxyz ";
    }

    void Start()
    {
        currentLives = startingLives;
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

        // update score UI
        UpdateScoreInformation();

        // create initial word panel - IT
        SetWordPanel(" IT ", false);
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
        bool wordValid = CheckWordValid(word);
        bool notUsedBefore = CheckWordNotUsedPreviously(word);
        int nKeptLetters = availableLetterPanel.nKeptLetters;

        // Calculate Score

        if (!notUsedBefore)
        {
            GameOver();
        }

        // Progress to next round
        if ((wordValid && notUsedBefore))
            ProgressRound(word, nKeptLetters);

        UpdateScoreInformation();
    }

    public void KeepButtonClicked()
    {
        if (!keepToggle)
        {
            keepToggle = true;
        } else
        {
            keepToggle = false;
        }
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

    void SetWordPanel(string str, bool addLetterDisplay)
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

            if (addLetterDisplay)
            {
                
            }
        }
    }

    void ClearWordPanel()
    {
        foreach (LetterSlotBig slot in wordPanel.transform.GetComponentsInChildren<LetterSlotBig>())
        {
            Destroy(slot.gameObject);
        }
    }

    bool CheckWordValid(string word)
    {
        return wordList.Contains(word.Trim());
    }

    bool CheckWordNotUsedPreviously(string word)
    {
        return !previousWorldList.Contains(word.Trim());
    }

    void ProgressRound(string word, int nKeptLetters)
    {
        gameRounds++;
        previousWorldList.Add(word.Trim());

        ClearWordPanel();
        availableLetterPanel.ClearLetters();

        SetWordPanel((" " + word.Trim() + " ").ToUpper(), true);

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

        // update lives according to length of word made
        currentLives += word.Trim().Length / 2;
    }

    void UpdateScoreInformation()
    {
        roundsText.text = "Rounds Complete: " + gameRounds.ToString();
        livesText.text = "Lives: " + currentLives.ToString();
    }

    void GameOver()
    {
        Debug.Log("Game Over");
    }
}
