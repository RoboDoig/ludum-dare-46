using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LetterSlotDisplay : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerUpHandler
{
    public float lerpSpeed;

    public RectTransform parentLayoutTransform;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private bool inDragMode = false;
    public bool available = true;
    public bool kept = false;

    private delegate void UpdateAction();
    private UpdateAction updateAction;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        updateAction = DefaultMode;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!kept)
        {
            inDragMode = true;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!kept)
            transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!kept)
        {
            inDragMode = false;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void DropSuccess()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
        available = false;
        updateAction = DroppedMode;
    }

    public void Undrop()
    {
        updateAction = DefaultMode;
        canvasGroup.alpha = 1;
        available = true;
        canvasGroup.blocksRaycasts = true;
    }

    // Update is called once per frame
    void Update()
    {
        updateAction();
    }

    void DefaultMode()
    {
        if (!inDragMode)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, parentLayoutTransform.anchoredPosition, Time.deltaTime * lerpSpeed);
        }
    }

    void DroppedMode()
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inDragMode = false;
        canvasGroup.blocksRaycasts = true;
    }
}
