using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UIManaging : MonoBehaviour
{
    public static bool gameIsStarted = false;
    public static bool gameIsPaused = false;

    public GameObject startScreen;
    public GameObject pauseMenuUI;
    public GameObject myPlayer;
    public GameObject targetPicker;
    public GameObject pauseButton;
    public AudioMixer audioMixer;
    public Animator startAnimator;
    public Animator endAnimator;

    void Start()
    {
        GameNotStarted();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsPaused && !gameIsStarted)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        //else
        //{
        //    Cursor.lockState = CursorLockMode.Locked;

        //}

        if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Cancel")) && gameIsStarted)
        {

            if (gameIsPaused)
            {

                Resume();
                //anim.SetBool("_isPaused", false);

            }
            else
            {

                Pause();
                //anim.SetBool("_isPaused", true);
            }
        }
    }

    public void StartGame()
    {
        Debug.Log("Game Started");
        startScreen.SetActive(false);
        myPlayer.SetActive(true);
        targetPicker.SetActive(true);
        Time.timeScale = 1f;
        gameIsStarted = true;
        pauseButton.SetActive(true);
    }

    public void GameNotStarted()
    {
        startScreen.SetActive(true);
        myPlayer.SetActive(false);
        targetPicker.SetActive(false);
        Time.timeScale = 0f;
        gameIsStarted = false;
        pauseButton.SetActive(false);
        startAnimator.updateMode = AnimatorUpdateMode.UnscaledTime; 
    }

    public void Resume()
    {

        Time.timeScale = 1f;
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        myPlayer.GetComponent<ClickToMove>().enabled = true;
        targetPicker.SetActive(true);
        pauseButton.SetActive(true);
        
    }

    public void Pause()
    {
        Time.timeScale = 0.00001f;
        myPlayer.GetComponent<ClickToMove>().enabled = false;
        targetPicker.SetActive(false);
        pauseMenuUI.SetActive(true);
        gameIsPaused = true;
        pauseButton.SetActive(false);
        startAnimator.updateMode = AnimatorUpdateMode.UnscaledTime; 
    }

    public void SetMenuStateIntro(int currentState)
    {

        startAnimator.SetInteger("MenuState", currentState);

    }

    public void SetMenuStateOutro(int currentState)
    {

        endAnimator.SetInteger("MenuState", currentState);

    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }

    public void SetVolumeMaster(float volume)
    {
        audioMixer.SetFloat("MasterVol", volume);
    }

    public void SetVolumeSfx(float volume)
    {
        audioMixer.SetFloat("SfxVol", volume);
    }
    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("MusicVol", volume);
    }

    public void SetVolumeAmbience(float volume)
    {
        audioMixer.SetFloat("AmbienceVol", volume);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("SceneFinal(0)");
    }

}
