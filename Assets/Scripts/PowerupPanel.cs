using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupPanel : MonoBehaviour
{

    public GameManager gameManager;

    bool timerPowerupActive = false;
    bool newLettersPowerupActive = false;
    bool freeVowelsPowerupActive = false;

    int timerPowerupCount = 0;
    int newLettersPowerupCount = 0;
    int freeVowelsPowerupCount = 0;

    public int newTimerPowerupEvery;
    public int newLetterRerollEvery;
    public int newFreeVowelEvery;

    public Button timerPowerupButton;
    public Button newLettersPowerupButton;
    public Button freeVowelsPowerupButton;

    public void TimerPowerup()
    {
        if (timerPowerupActive)
        {
            gameManager.TimerPowerup();
            timerPowerupActive = false;
            timerPowerupCount = 0;
            timerPowerupButton.GetComponent<Image>().color = Color.black;
            timerPowerupButton.GetComponentInChildren<Text>().color = Color.black;
        }
    }

    public void NewLettersPowerup()
    {
        if (newLettersPowerupActive)
        {
            gameManager.NewLettersPowerup();
            newLettersPowerupActive = false;
            newLettersPowerupCount = 0;
            newLettersPowerupButton.GetComponent<Image>().color = Color.black;
            newLettersPowerupButton.GetComponentInChildren<Text>().color = Color.black;
        }
    }

    public void FreeVowelsPowerup()
    {
        if (freeVowelsPowerupActive)
        {
            gameManager.FreeVowelsPowerup();
            freeVowelsPowerupActive = false;
            freeVowelsPowerupCount = 0;
            freeVowelsPowerupButton.GetComponent<Image>().color = Color.black;
            freeVowelsPowerupButton.GetComponentInChildren<Text>().color = Color.black;
        }
    }

    public void UpdatePowerupPanel()
    {
        timerPowerupCount++;
        if (timerPowerupCount >= newTimerPowerupEvery)
        {
            timerPowerupActive = true;
            timerPowerupButton.GetComponent<Image>().color = Color.white;
            timerPowerupButton.GetComponentInChildren<Text>().color = Color.white;
        } 

        newLettersPowerupCount++;
        if (newLettersPowerupCount >= newLetterRerollEvery)
        {
            newLettersPowerupActive = true;
            newLettersPowerupButton.GetComponent<Image>().color = Color.white;
            newLettersPowerupButton.GetComponentInChildren<Text>().color = Color.white;
        }

        freeVowelsPowerupCount++;
        if (freeVowelsPowerupCount >= newFreeVowelEvery)
        {
            freeVowelsPowerupActive = true;
            freeVowelsPowerupButton.GetComponent<Image>().color = Color.white;
            freeVowelsPowerupButton.GetComponentInChildren<Text>().color = Color.white;
        }
    }

    public void ResetPanel()
    {
        timerPowerupActive = true;
        newLettersPowerupActive = true;
        freeVowelsPowerupActive = true;

        TimerPowerup();
        NewLettersPowerup();
        FreeVowelsPowerup();
    }
}
