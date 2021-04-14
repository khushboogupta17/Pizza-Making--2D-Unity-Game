using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager instance;
    public GameObject customerprefab;
    public GameObject parentCustomer;
    public TextureDivider textureDivider;
    public int customerCount = 4;
    public static bool servedWrong = false;

    [HideInInspector]
    public GameObject currentCust;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        NextCustomer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextCustomer()
    { 
        StartCoroutine(StartAfterDelay());
    
    }

    IEnumerator StartAfterDelay()
    {
        yield return new WaitForSecondsRealtime(1f);
        if (currentCust != null)
            Destroy(currentCust);

        if (customerCount <= 0)
        {
            UIManager.uIManager.LevelCompleted();
            yield break;
        }

        yield return new WaitForSecondsRealtime(1f);

        if (servedWrong)
        {
            customerCount++;
            servedWrong = false;
        }

        customerCount--;
        textureDivider.ResetVar();
        currentCust = Instantiate(customerprefab, parentCustomer.transform);
        currentCust.transform.localPosition = Vector3.zero;
        currentCust.GetComponent<Customer>().PlaceOrder();
    }
}
