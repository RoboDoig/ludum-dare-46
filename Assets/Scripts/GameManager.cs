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
    public Text timerText;
    public Text roundSummaryText;
    public Text failMessageText;
    public GameObject gameOverPanel;

    public TextAsset wordsAsset;
    string[] wordArray;
    List<string> wordList;
    List<string> previousWorldList;
    string letterChoice;

    [Header("Game Parameters")]
    public int minNewLettersPerRound = 1; 
    public int maxNewLettersPerRound = 4;
    public float defaultTimerLength = 30f; // default amount of time per round
    public float timerMultiplier = 0.1f;

    int gameRounds = 0;
    int score = 0;
    bool keepToggle = false;
    float timer;
    float timerSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        char[] archDelim = new char[] { '\r', '\n' };
        wordArray = wordsAsset.text.Split(archDelim);
        wordList = new List<string>(wordArray);

        previousWorldList = new List<string>();
        previousWorldList.Add("it");

        letterChoice = "abcdefghijklmnopqrstuvwxyz ";

        gameOverPanel.SetActive(false);
    }

    void Start()
    {
        // Begin new round
        // how many letters do we get this round?
        int newLettersThisRound = Random.Range(minNewLettersPerRound, maxNewLettersPerRound);

        // choose those letters
        string newLetters = "";
        for (int i = 0; i < newLettersThisRound; i++)
        {
            newLetters += letterChoice[Random.Range(0, letterChoice.Length)];
        }

        // show them in the UI
        availableLetterPanel.InitialiseLetters(newLetters);

        // update score UI
        UpdateScoreInformation();

        // create initial word panel - IT
        SetWordPanel(" IT ", false);

        // set timer
        timer = defaultTimerLength;
        timerSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime * timerSpeed;
        if(timer < 0)
        {
            timer = 0;
            GameOver("You ran out of time");
        }
        UpdateTimerInformation();
    }

    public void AliveButtonClicked()
    {
        // Get the current word
        string word = wordPanel.GetWord();

        // Scoring logic
        bool wordValid = CheckWordValid(word);
        bool notUsedBefore = CheckWordNotUsedPreviously(word);
        string keptLetters = availableLetterPanel.GetKeptLetters();

        // Calculate Score

        if (!notUsedBefore)
        {
            GameOver("You repeated the word: " + word.ToUpper());
        }

        // Progress to next round
        if ((wordValid && notUsedBefore))
            ProgressRound(word, keptLetters);

        UpdateScoreInformation();
    }

    public void KeepButtonClicked()
    {
        if (!keepToggle)
        {
            keepToggle = true;
            availableLetterPanel.KeepLetters();
        } else
        {
            keepToggle = false;
            availableLetterPanel.UnKeepLetters();
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
        return wordList.Contains(word.Trim().ToUpper());
    }

    bool CheckWordNotUsedPreviously(string word)
    {
        return !previousWorldList.Contains(word.Trim());
    }

    void ProgressRound(string word, string keptLetters)
    {
        gameRounds++;
        previousWorldList.Add(word.Trim());

        // how many letters do we get this round?
        //int newLettersThisRound = Random.Range(minNewLettersPerRound, maxNewLettersPerRound);
        int newLettersThisRound = availableLetterPanel.CountAvailableLetters() + Random.Range(1, 3);

        ClearWordPanel();
        wordPanel.Shake();

        availableLetterPanel.ClearLetters();

        SetWordPanel((" " + word.Trim() + " ").ToUpper(), true);

        // choose those letters
        string newLetters = "";
        for (int i = 0; i < newLettersThisRound; i++)
        {
            newLetters += letterChoice[Random.Range(0, letterChoice.Length)];
        }
        newLetters = keptLetters + newLetters;

        // show them in the UI
        availableLetterPanel.InitialiseLetters(newLetters);

        keepToggle = false;

        // reset timer
        timer = defaultTimerLength;
        timerSpeed = 1 + (newLetters.Length * timerMultiplier);
    }

    void UpdateScoreInformation()
    {
        roundsText.text = "Rounds Complete: " + gameRounds.ToString();
    }

    void UpdateTimerInformation()
    {
        timerText.text = "Time Left: " + timer.ToString();
    }

    void GameOver(string failMessage)
    {
        gameOverPanel.SetActive(true);
        roundSummaryText.text = "You lasted " + gameRounds.ToString() + " rounds";
        failMessageText.text = failMessage;
    }

    public void ResetGame()
    {
        gameOverPanel.SetActive(false);

        previousWorldList = new List<string>();

        ClearWordPanel();
        previousWorldList.Add("it");

        availableLetterPanel.ClearLetters();

        gameRounds = 0;
        // Begin new round
        // how many letters do we get this round?
        int newLettersThisRound = Random.Range(minNewLettersPerRound, maxNewLettersPerRound);

        // choose those letters
        string newLetters = "";
        for (int i = 0; i < newLettersThisRound; i++)
        {
            newLetters += letterChoice[Random.Range(0, letterChoice.Length)];
        }

        // show them in the UI
        availableLetterPanel.InitialiseLetters(newLetters);

        // update score UI
        UpdateScoreInformation();

        // create initial word panel - IT
        SetWordPanel(" IT ", false);

        // set timer
        timer = defaultTimerLength;
        timerSpeed = 1f;
    }

    public void TimerPowerup()
    {
        //timer += defaultTimerLength;
    }

    public void NewLettersPowerup()
    {

    }

    public void FreeVowelPowerup()
    {

    }
}
