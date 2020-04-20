using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public float fadeSpeed;

    public Image keepButtonImage;
    public Text keepButtonText;

    public Image wordPanelImage;
    public Image panel1;
    public Image panel2;
    public Text panel2Text;
    public Image panel3;
    public Text panel3Text;
    public Image panel4;

    public Image aliveButtonImage;
    public Text aliveButtonText;

    public Image roundsCompleteImage;
    public Text roundsCompleteText;

    public Image timerImage;

    public Image powerup1;
    public Image powerup2;
    public Image powerup3;

    public Image startPanelImage;
    public Image startButtonImage;
    public Text startButtonText;
    public Image tutorialButtonImage;
    public Text tutorialButtonText;

    public Transform tutorialPanel;

    public Color transparent;
    public Color visible;
    public Color blackVisible;
    float timeCounter = 0f;

    private delegate void UpdateAction();
    private UpdateAction updateAction;

    void Start()
    {
        SetColorGroup1(transparent);
        SetColorGroup2(transparent);
        SetColorGroup3(transparent);
        SetColorGroup4(transparent);
        SetColorGroup5(transparent, transparent);

        updateAction = Stage1;
    }

    void Update()
    {
        updateAction();
    }

    void Stage1()
    {
        timeCounter += Time.deltaTime * fadeSpeed;
        SetColorGroup1(Color.Lerp(transparent, visible, timeCounter));

        if (timeCounter >= 1)
        {
            timeCounter = 0;
            updateAction = Stage2;
        }
    }

    void Stage2()
    {
        timeCounter += Time.deltaTime * fadeSpeed;
        SetColorGroup2(Color.Lerp(transparent, visible, timeCounter));

        if (timeCounter >= 1)
        {
            timeCounter = 0;
            updateAction = Stage3;
        }
    }

    void Stage3()
    {
        timeCounter += Time.deltaTime * fadeSpeed;
        SetColorGroup3(Color.Lerp(transparent, visible, timeCounter));

        if (timeCounter >= 1)
        {
            timeCounter = 0;
            updateAction = Stage4;
        }
    }

    void Stage4()
    {
        timeCounter += Time.deltaTime * fadeSpeed;
        SetColorGroup4(Color.Lerp(transparent, visible, timeCounter));

        if (timeCounter >= 1)
        {
            timeCounter = 0;
            startButtonImage.GetComponent<Button>().interactable = true;
            tutorialButtonImage.GetComponent<Button>().interactable = true;
            updateAction = Stage5;
        }
    }

    void Stage5()
    {
        timeCounter += Time.deltaTime * fadeSpeed;
        SetColorGroup5(Color.Lerp(transparent, visible, timeCounter), Color.Lerp(transparent, blackVisible, timeCounter));

        if (timeCounter >= 1)
        {
            updateAction = Stage6;
        }
    }

    void Stage6()
    {

    }

    void SetColorGroup1(Color color)
    {
        keepButtonImage.color = color;
        keepButtonText.color = color;
    }

    void SetColorGroup2(Color color)
    {
        wordPanelImage.color = transparent;
        panel1.color = color;
        panel2.color = color;
        panel3.color = color;
        panel4.color = color;
        panel2Text.color = color;
        panel3Text.color = color;
    }

    void SetColorGroup3(Color color)
    {
        aliveButtonImage.color = color;
        aliveButtonText.color = color;
    }

    void SetColorGroup4(Color color)
    {
        roundsCompleteImage.color = color;
        roundsCompleteText.color = color;
        timerImage.color = color;

        powerup1.color = color;
        powerup2.color = color;
        powerup3.color = color;
    }

    void SetColorGroup5(Color color1, Color color2)
    {
        startPanelImage.color = color2;
        startButtonImage.color = color1;
        startButtonText.color = color1;
        tutorialButtonImage.color = color1;
        tutorialButtonText.color = color1;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ShowTutorial()
    {
        tutorialPanel.gameObject.SetActive(true);
    }

    public void CloseTutorial()
    {
        tutorialPanel.gameObject.SetActive(false);
    }
}
