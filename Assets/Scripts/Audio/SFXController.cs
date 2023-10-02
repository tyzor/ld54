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

    public static void PlaySoundUnique(SFX sfx, float volume = 1f)
    {
        if(_instance._sfxTracker.ContainsKey(sfx) 
            && _instance._sfxTracker[sfx])
        {
            _instance.StartCoroutine(_instance.clearSoundTracker(sfx));
            return;
        }

        _instance._sfxTracker[sfx] = true;

        _instance.TryPlaySound(sfx, volume);
    }

    private IEnumerator clearSoundTracker(SFX sfx)
    {
        yield return new WaitForSeconds(_instance._sfxDatas[sfx].clip.length);
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
}
