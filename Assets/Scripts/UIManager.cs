using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager uIManager;
    public TextMeshProUGUI errorText;
    public TextureDivider textureDivider;
    public TMP_InputField partsCut;
    public TextMeshProUGUI coinsText;
    public GameObject LevelCompletedPanel;
    public TextMeshProUGUI levelCompletedPointsEarned;
    public GameObject pausePanel;
    public ParticleSystem coinsIncreasedEffect;
    public ParticleSystem coinsDecreasedEffect;
    public Animator anim;
    int coins;
    // Start is called before the first frame update
    void Start()
    {
        coins = 0;
        if (uIManager == null)
        {
            uIManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetError(string error)
    {
        errorText.gameObject.SetActive(true);
        errorText.text = error;
        StartCoroutine(DisableAfter(2f, errorText.gameObject));
    }

    IEnumerator DisableAfter(float time,GameObject obj)
    {
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Serve()
    {
        Debug.Log("Parts actually cut " + int.Parse(partsCut.text));
        
        if (CustomerManager.instance.currentCust.GetComponent<Customer>().IsOrderCorrect(int.Parse(partsCut.text)))
        {
            coins += 100;
            coinsIncreasedEffect.Play();
            //show success animation
        }
        else
        {
            SetError("Uh,oh,You might have served wrong portion to customer");
            if(coins>=50)
            coins -= 50;
            CustomerManager.servedWrong = true;
            coinsDecreasedEffect.Play();
        }
       // AudioManager.instance.Sound().Play();
        partsCut.text = "";
        coinsText.text = coins.ToString();
        
    CustomerManager.instance.NextCustomer();
    }

   
    public void Cut()
    {
        textureDivider.SetCutsAndDivide(int.Parse(partsCut.text));
    }

    public void LevelCompleted()
    {
        //LevelCompletedPanel.SetActive(true);
        anim.SetTrigger("LevelCompletedPanel");
        levelCompletedPointsEarned.text = coins.ToString();
    }

    public void Pause(bool pause)
    {
        if (pause == true)
        {
            anim.SetTrigger("PausePanel");
            //pausePanel.SetActive(true);
        }
        else
        {
            Debug.Log("pause panel disabling");
            pausePanel.SetActive(false);
        }
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Home()
    {
        GameManager.instance.LoadScene(0);
    }
}
