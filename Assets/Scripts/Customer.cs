using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
public class Customer : MonoBehaviour, IDropHandler
{
    public TextMeshProUGUI OrderText;
    public Sprite sadFace;
    public Sprite happyFace;
    public Sprite normalFace;
    // public TextureDivider textureDivider;

    [HideInInspector]
    public int num, den;
        
    [HideInInspector]
    public float orderAmt;
    float orderReceived;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlaceOrder()
    {
        num = 1;
        den = 0;
        GetComponent<Image>().sprite = normalFace;
        orderReceived = 0;
        while (num > den)
        {
            num = Random.Range(1, 5);
            den = Random.Range(3, 5);
        }
        
        orderAmt = (float)num;
        OrderText.text = "I want " + num+ "/"+den;
        OrderText.gameObject.SetActive(true);
       
    }

   void LifePeriod()
    {
        //set some random time limit
        //if not fed between that time limit then leave
        
    }

    void Deactivate()
    {
      
        //if fed within time limit or nor fed
        //destroy
    }

    public void TakeOrder()
    {
        Debug.Log("Order received");

    }

    public bool IsOrderCorrect(int partsActuallyCut)
    {
        Debug.Log(" PartActuallyCut"+ partsActuallyCut+ " den"+den);

        if(orderReceived == orderAmt && partsActuallyCut == den)
        {
            GetComponent<Image>().sprite = happyFace;
            OrderText.gameObject.transform.parent.gameObject.SetActive(false);
              return true;
        }
        else
        {
            GetComponent<Image>().sprite = sadFace;
            OrderText.gameObject.transform.parent.gameObject.SetActive(false);
            return false;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("found pizza");
       if(eventData.pointerDrag != null)
        {
                orderReceived ++;
            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
            eventData.pointerDrag.SetActive(false);
            
        }
    }
}
