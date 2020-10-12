using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour,IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler
{

    private Canvas canvas;

    [SerializeField]
    private RectTransform startPos;

    private GameObject[] teamSlots;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private List<RaycastResult> results = new List<RaycastResult>();

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        teamSlots = GameObject.FindGameObjectsWithTag("TeamSlot");

        foreach(GameObject slot in teamSlots)
        {
            if (slot.GetComponent<TeamSlot>().GetPlayer() == null)
            {
                startPos = slot.GetComponent<RectTransform>();
                slot.GetComponent<TeamSlot>().SetPlayer(this.gameObject);
                GetComponent<RectTransform>().position = startPos.position;
                return;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        PointerEventData pointerData = new PointerEventData(EventSystem.current) { pointerId = -1 };
        pointerData.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointerData, results);
        bool slot=false;
        RectTransform newPos=new RectTransform();
        foreach(RaycastResult res in results)
        {
            if (res.gameObject.tag == "TeamSlot")
            {
                slot = true;
                newPos = res.gameObject.GetComponent<RectTransform>();
                break;
            }
               
        }
        if (!slot)
        {
            rectTransform.anchoredPosition = startPos.anchoredPosition;
        }
        else
        {
            if(newPos.gameObject != null)
            {
                if (newPos.gameObject.GetComponent<TeamSlot>().GetPlayer() == null)
                {
                    //deleting old player ref and setting new ref
                    startPos.gameObject.GetComponent<TeamSlot>().SetPlayer(null);
                    newPos.gameObject.GetComponent<TeamSlot>().SetPlayer(this.gameObject);
                    SetStartPos(newPos);
                }
                else
                {
                    GameObject temp = newPos.gameObject.GetComponent<TeamSlot>().GetPlayer();
                    startPos.gameObject.GetComponent<TeamSlot>().SetPlayer(temp);
                    temp.GetComponent<DragDrop>().SetStartPos(startPos);
                    newPos.gameObject.GetComponent<TeamSlot>().SetPlayer(this.gameObject);
                    SetStartPos(newPos);
                }
            }
         
        }
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void SetStartPos(RectTransform pos)
    {
        this.startPos = pos;
        rectTransform.position = startPos.position;
    }
 
   
}
