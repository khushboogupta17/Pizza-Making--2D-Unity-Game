using UnityEngine.UI;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    public AudioSource SoundAudio;
    public AudioSource MusicAudio;
    
  


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        MusicAudio.Play();
        
    }

    public AudioSource Sound()
    {
        return SoundAudio;
        
    }

    public AudioSource Music()
    {
        return MusicAudio;

    }
}
