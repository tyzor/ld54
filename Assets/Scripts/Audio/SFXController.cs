using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;


public enum SFX
{
    NONE = 0,
    DING = 1,
    CRANK = 2,
    BUTTON_CLICK = 3,
    FLOOR_DING = 4,
    GAME_OVER = 20,

}

public class SFXController : MonoBehaviour
{
    //============================================================================================================//

    [Serializable]
    public struct SFXData
    {
        public string name;
        public SFX type;
        public AudioClip clip;
    }

    //============================================================================================================//

    private static SFXController _instance;
    private Dictionary<SFX, SFXData> _sfxDatas;

    private Dictionary<SFX, bool> _sfxTracker;

    public static void PlaySound(SFX sfx, float volume = 1f)
    {
        _instance.TryPlaySound(sfx, volume);
    }

    // Only play sound if it's not already playing
    public static void PlaySoundUnique(SFX sfx, float volume = 1f, float delay = 0)
    {
        if(_instance._sfxTracker.ContainsKey(sfx) 
            && _instance._sfxTracker[sfx])
        {
            return;
        }        

        // Default delay is the length of the clip
        if(delay <= 0)
            delay = _instance._sfxDatas[sfx].clip.length;

        _instance.StartCoroutine(_instance.SoundTracker(sfx, volume, delay));
    }

    private IEnumerator SoundTracker(SFX sfx, float volume, float delay)
    {
        _instance.TryPlaySound(sfx, volume);
        _instance._sfxTracker[sfx] = true;
        yield return new WaitForSecondsRealtime(delay);
        _instance._sfxTracker[sfx] = false;
    }

    //============================================================================================================//

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private SFXData[] sfx;

    //Unity Functions
    //============================================================================================================//

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        
        _sfxDatas = new Dictionary<SFX, SFXData>();
        _sfxTracker = new Dictionary<SFX, bool>();
        foreach (var sfxData in sfx)
        {
            _sfxDatas.Add(sfxData.type, sfxData);
        }
    }

    private void Start()
    {

    }

    //============================================================================================================//

    private void TryPlaySound(SFX sfx, float volume = 1f)
    {
        if (sfx == SFX.NONE) { return; }

        // get specified sound
        SFXData data = _sfxDatas[sfx];

        // play sound
        audioSource?.PlayOneShot(data.clip, volume);
    }
    
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
