using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragandDrop : MonoBehaviour, IBeginDragHandler, IDragHandler//, IEndDragHandler
{
    private RectTransform rectTransform;
    private RectTransform window;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Window win = GetComponentInParent<Window>();
        if (win != null)
            window = win.GetComponent<RectTransform>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if(window != null)
        {
            window.SetAsLastSibling();
        }
        else
        {
            transform.SetAsLastSibling();
        } 
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(window != null)
        {
            window.anchoredPosition += eventData.delta;
        }
        else
        {
            rectTransform.anchoredPosition += eventData.delta;
        }
        
    }


    // public void OnEndDrag(PointerEventData eventData)
    // {
    //     GameObject snapPoint = GameObject.Find(cupSnap);

    //     if (snapPoint != null)
    //     {
    //         float distance = Vector2.Distance(rectTransform.position, snapPoint.transform.position);
            
    //         if (distance < snapDistance)
    //         {
    //             rectTransform.position = snapPoint.transform.position;
    //             transform.SetParent(snapPoint.transform);
    //         }
    //     }
    // }

}
