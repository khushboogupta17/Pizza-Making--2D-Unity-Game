using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioBehaviour bgSound;

    public Animator anim;
    public static bool levelCompleted = false;
    int levelIndex = 0;



 
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
            return;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(int ind)
    {
        levelIndex = ind;
        anim.SetTrigger("fade_out");
       // SceneManager.LoadScene(ind);
    }



public void OnFadeComplete()
{
        transform.GetChild(0).gameObject.SetActive(false);
    SceneManager.LoadScene(levelIndex);
  
}


    public void Quit()
    {
        Application.Quit();
    }
}
