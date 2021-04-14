using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class InputManager : MonoBehaviour
{
    private bool draggingItem = false;
    private GameObject draggedObject;
    private Vector2 touchOffset;

    List<RaycastResult> hitObjects = new List<RaycastResult>();

    void Update()
    {
        Debug.DrawRay(CurrentTouchPosition, Vector3.forward*10f, Color.blue);

        // LayerMask.NameToLayer("Customer")
        RaycastHit[] ray = Physics.RaycastAll(CurrentTouchPosition, Vector3.forward*10f, 10f);
        GameObject obj = GetObjectUnderMouse();
        if (obj!=null)
        {
            Debug.Log(obj.name);
            //ray[0].collider.gameObject.GetComponent<Customer>().TakeOrder();
        }
        if (HasInput)
        {
            DragOrPickUp();
        }
        else
        {
            if (draggingItem)
                DropItem();
        }
    }

    Vector2 CurrentTouchPosition
    {
        get
        {
            Vector2 inputPos;
            inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return inputPos;
        }
    }

    GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;
        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0)
            return null;
        else return hitObjects[0].gameObject;
    }
    private void DragOrPickUp()
    {
        var inputPosition = CurrentTouchPosition;
        Debug.DrawRay(inputPosition, Vector3.forward, Color.red);
        if (draggingItem)
        {
            draggedObject.transform.position = inputPosition + touchOffset;
            RaycastHit2D[] ray= Physics2D.RaycastAll(Input.mousePosition, Vector3.forward,10f, LayerMask.NameToLayer("Customer"));
           
            if (ray.Length > 0)
            {
                Debug.Log(ray[0].collider.gameObject.name);
                if(ray[0].collider.gameObject.GetComponent<Customer>()!=null)
                ray[0].collider.gameObject.GetComponent<Customer>().TakeOrder();
            }
            
            
        }
        else
        {
            RaycastHit2D[] touches = Physics2D.RaycastAll(inputPosition, inputPosition, 0.5f);
            if (touches.Length > 0)
            {
                var hit = touches[0];
                if (hit.transform != null)
                {
                    draggingItem = true;
                    draggedObject = hit.transform.gameObject;
                    touchOffset = (Vector2)hit.transform.position - inputPosition;
                    draggedObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                }
            }
        }
    }

    private bool HasInput
    {
        get
        {
            // returns true if either the mouse button is down or at least one touch is felt on the screen
            return Input.GetMouseButton(0);
        }
    }

    void DropItem()
    {
        draggingItem = false;
        draggedObject.transform.localScale = new Vector3(1f, 1f, 1f);

    }
}