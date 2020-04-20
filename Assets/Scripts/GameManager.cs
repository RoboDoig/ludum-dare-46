using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public WordPanel wordPanel;
    public Button keepButton;
    public Button aliveButton;

    public AvailableLetterPanel availableLetterPanel;
    public PowerupPanel powerupPanel;
    public Image timerImage;
    public GameObject letterSlot;
    public Text roundsText;
    public Text roundSummaryText;
    public Text failMessageText;
    public GameObject gameOverPanel;

    public TextAsset wordsAsset;
    string[] wordArray;
    List<string> wordList;
    List<string> previousWorldList;
    string letterChoice;
    AudioManager audioManager;

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
    float timerMaxThisRound;

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

        audioManager = GetComponent<AudioManager>();
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
        timerMaxThisRound = timer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime * timerSpeed;

        audioManager.TimeRunningOut(timer / timerSpeed);

        if(timer < 0)
        {
            timer = 0;
            GameOver("T I M E R    D E P L E T E D");
        }

        float timerFraction = timer / timerMaxThisRound;
        timerImage.fillAmount = timerFraction;
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
            string wordDisplay = "";
            foreach(char character in word)
            {
                wordDisplay += (character + " ");
            }
            GameOver("W O R D    R E P E A T E D:    " + wordDisplay.ToUpper());

        }

        if (!wordValid)
        {
            audioManager.Fail();
            wordPanel.Shake();
        }

        // Progress to next round
        if ((wordValid && notUsedBefore))
        {
            audioManager.FadeOutTimeRunningOut();
            audioManager.RoundEnd();

            ProgressRound(word, keptLetters);
        }
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
            slot.transform.SetParent(wordPanel.transform, false);

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
        wordPanel.ColourChange();

        gameRounds++;
        previousWorldList.Add(word.Trim());

        // how many letters do we get this round?
        //int newLettersThisRound = Random.Range(minNewLettersPerRound, maxNewLettersPerRound);
        int newLettersThisRound = availableLetterPanel.CountAvailableLetters() + Random.Range(1, 3);

        ClearWordPanel();

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
        timerMaxThisRound = timer;

        // update powerups
        powerupPanel.UpdatePowerupPanel();
    }

    void UpdateScoreInformation()
    {
        roundsText.text = gameRounds.ToString();
    }

    void GameOver(string failMessage)
    {
        audioManager.GameOver();
        gameOverPanel.SetActive(true);
        roundSummaryText.text = "A F T E R    " + gameRounds.ToString() + "    R O U N D S";
        failMessageText.text = failMessage;
        powerupPanel.ResetPanel();
        Time.timeScale = 0f;
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;

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
        timerMaxThisRound = timer;
        timerSpeed = 1f;
    }

    public void TimerPowerup()
    {
        timer += defaultTimerLength;
        timerMaxThisRound = timer;
    }

    public void NewLettersPowerup()
    {
        // how many letters do we get this round?
        int newLettersThisRound = availableLetterPanel.CountTotalLetters();

        // choose those letters
        string newLetters = "";
        for (int i = 0; i < newLettersThisRound; i++)
        {
            newLetters += letterChoice[Random.Range(0, letterChoice.Length)];
        }

        // show them in the UI
        availableLetterPanel.ClearLetters();
        availableLetterPanel.InitialiseLetters(newLetters);
    }

    public void FreeVowelsPowerup()
    {
        string vowelChoice = "aeiou";
        int nVowels = Random.Range(1, 3);
        string newLetters = "";
        for (int i = 0; i < nVowels; i++)
        {
            newLetters += vowelChoice[Random.Range(0, vowelChoice.Length)];
        }

        availableLetterPanel.AddLetters(newLetters);
    }

    void StartSequence()
    {

    }

    public void GoToStartMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
