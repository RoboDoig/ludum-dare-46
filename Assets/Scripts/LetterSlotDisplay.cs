using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LetterSlotDisplay : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public RectTransform parentLayoutTransform;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private bool inDragMode = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        inDragMode = true;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        inDragMode = false;
        canvasGroup.blocksRaycasts = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inDragMode)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, parentLayoutTransform.anchoredPosition, Time.deltaTime);
        }
    }
}
