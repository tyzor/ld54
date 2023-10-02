using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] 
    private Slider musicVolumeSlider;

    [SerializeField] 
    private Slider sfxVolumeSlider;

    [SerializeField]
    private Button resumeButton;

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        InitControls();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitControls()
    {
        resumeButton.onClick.AddListener(OnResumePressed);   
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

        musicVolumeSlider.value = -20f;
        sfxVolumeSlider.value = -20f;
    }

    private void OnResumePressed()
    {
        gameManager.UnpauseGame();
    }
    private void OnMusicVolumeChanged(float volume)
    {
        MusicController.SetVolume(volume);
    }
    private void OnSFXVolumeChanged(float volume)
    {
        SFXController.SetVolume(volume);
    }

    public void showPause()
    {
        Debug.Log("ShowPause");
        pauseScreen.SetActive(true);
    }

    public void hidePause()
    {
        Debug.Log("HidePause");
        pauseScreen.SetActive(false);
    }
}
