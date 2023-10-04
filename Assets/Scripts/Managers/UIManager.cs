using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    TextMeshProUGUI gameOverText;
    [SerializeField]
    Button restartGameButton;

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

        restartGameButton.onClick.AddListener(OnGameRestart);
        gameOverScreen.SetActive(false);

    }

    private void OnResumePressed()
    {
        gameManager.UnpauseGame();
    }
    private void OnMusicVolumeChanged(float volume)
    {
        MusicController.SetPercentVolume(volume);
    }
    private void OnSFXVolumeChanged(float volume)
    {
        SFXController.PlaySoundUnique(SFX.BUTTON_CLICK);
        SFXController.SetPercentVolume(volume);
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

    private void OnGameRestart()
    {
        // Restart Game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void showGameEnd(string endMessage)
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        gameOverText.SetText(endMessage);
        gameOverScreen.SetActive(true);
    }
}
