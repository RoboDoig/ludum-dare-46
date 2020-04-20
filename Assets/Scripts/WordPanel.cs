using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordPanel : MonoBehaviour
{
    [Header("Shake Attributes")]
    public float shakeSpeed;
    public float shakeAmount;
    public float shakeTime;

    [Header("Colour Attributes")]
    public float colourSpeed;
    public float colourTime;
    public Color fromColour;
    public Color toColour;

    private float shakeTimer;
    private Vector3 startPos;

    private float colourTimer;
    private Image borderImage;
    private float colourCount;

    private delegate void UpdateAction();
    private UpdateAction updateAction;

    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        borderImage = GetComponent<Image>();
        updateAction = DefaultUpdate;
    }

    // Update is called once per frame
    void Update()
    {
        updateAction();
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

    public void Shake()
    {
        updateAction = ShakeUpdate;
        shakeTimer = shakeTime;
        startPos = rectTransform.anchoredPosition;
    }

    public void ColourChange()
    {
        updateAction = ColourUpdate;
        colourTimer = colourTime;
        colourCount = 0f;
        LetterSlotBig[] letterPanels = gameObject.GetComponentsInChildren<LetterSlotBig>();
    }

    void DefaultUpdate()
    {

    }

    void ShakeUpdate()
    {
        shakeTimer -= Time.deltaTime;
        rectTransform.anchoredPosition = new Vector2(startPos.x + (Mathf.Sin(Time.time * shakeSpeed) * shakeAmount), rectTransform.anchoredPosition.y);
        if (shakeTimer <= 0f)
        {
            rectTransform.anchoredPosition = startPos;
            updateAction = DefaultUpdate;
        }
    }

    void ColourUpdate()
    {
        colourCount += Time.deltaTime * colourSpeed;
        colourTimer -= Time.deltaTime;
        borderImage.color = Color.Lerp(fromColour, toColour, Mathf.PingPong(colourCount, 1));
        if (colourTimer <= 0f)
        {
            borderImage.color = fromColour;
            updateAction = DefaultUpdate;
        }
    }
}
