using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordPanel : MonoBehaviour
{
    public float shakeSpeed;
    public float shakeAmount;
    public float shakeTime;

    private float shakeTimer;
    private Vector3 startPos;

    private delegate void UpdateAction();
    private UpdateAction updateAction;

    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
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

    void DefaultUpdate()
    {

    }

    void ShakeUpdate()
    {
        Debug.Log("Shaking");
        shakeTimer -= Time.deltaTime;
        rectTransform.anchoredPosition = new Vector2(startPos.x + (Mathf.Sin(Time.time * shakeSpeed) * shakeAmount), rectTransform.anchoredPosition.y);
        if (shakeTimer <= 0f)
        {
            rectTransform.anchoredPosition = startPos;
            updateAction = DefaultUpdate;
        }
    }
}
