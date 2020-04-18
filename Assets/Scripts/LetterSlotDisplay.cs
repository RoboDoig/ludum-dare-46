using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LetterSlotDisplay : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public RectTransform parentLayoutTransform;
    private RectTransform rectTransform;

    private bool inDragMode = false;

    public void OnDrag(PointerEventData eventData)
    {
        inDragMode = true;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        inDragMode = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //rectTransform.anchoredPosition = parentLayoutTransform.anchoredPosition;

        if (!inDragMode)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, parentLayoutTransform.anchoredPosition, Time.deltaTime);
        }
    }
}
