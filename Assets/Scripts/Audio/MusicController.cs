
using System;
using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    private static MusicController _instance;
    
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioMixer audioMixer;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
    }

    //============================================================================================================//

    // Set decibel level directly
    public static void SetVolume(float volume)
    {
        _instance.audioMixer.SetFloat("Volume", volume);
    }

    // Takes a volume percentage and converts into an appropriate decibel level
    // volume - specified between 0.0001 and 1
    public static void SetPercentVolume(float volume)
    {
        // Safety check (log zero is undefined)
        if(volume <= 0)
            volume = 0.0001f;
        if(volume > 1)
            volume = 1;
        _instance.audioMixer.SetFloat("Volume", Mathf.Log10(volume)*20f);
    }
    

}
