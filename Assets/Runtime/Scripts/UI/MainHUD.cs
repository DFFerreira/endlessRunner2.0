using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainHUD : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] GameMode gameMode;
    [SerializeField] HUDAudioController hudAudioController;
    [SerializeField] MusicPlayer musicPlayer;

    [Header("Texts")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI distanceText;
    [SerializeField] TextMeshProUGUI countdownText;

    [Header("Buttons")]
    [SerializeField] Button pauseButton;
    [SerializeField] Button resumeButton;
    [SerializeField] Button runButton;

    [Header("Overlays")]
    [SerializeField] GameObject pauseOverlay;
    [SerializeField] GameObject gameOverlay;
    [SerializeField] GameObject startOverlay;

    void Start()
    {
        pauseOverlay.SetActive(false);
        gameOverlay.SetActive(false);
        startOverlay.SetActive(true);
    }



    void LateUpdate()
    {
        scoreText.text = $"Score: {player.Score}";
        distanceText.text = $"{player.DistanceTravelled}m";
    }

    public void Pause()
    {
        musicPlayer.PauseMusic();
        hudAudioController.PlayButtonAudio();
        pauseButton.interactable = false;        
        pauseOverlay.SetActive(true);
        gameOverlay.SetActive(false);
        resumeButton.interactable = true;
        gameMode.PauseGame();
    }

    public void Resume()
    {
        musicPlayer.UnpauseMusic();
        hudAudioController.PlayButtonAudio();
        resumeButton.interactable = false;
        gameOverlay.SetActive(true);
        pauseOverlay.SetActive(false);
        pauseButton.interactable = true;
        gameMode.ResumeGame();
    }

    public void Run()
    {
        hudAudioController.PlayButtonAudio();
        gameOverlay.SetActive(true);
        startOverlay.SetActive(false);
        gameMode.StartGame();
    }

    public IEnumerator PlayStartCoroutine(int countdownSeconds)
    {
        Resume();
        countdownText.gameObject.SetActive(false);

        if(countdownSeconds <= 0)
        {
            yield break;
        }

        float timeToStart = Time.time + countdownSeconds;
        //yield return null;

        countdownText.gameObject.SetActive(true);
        int previousRemaininTimeInt = countdownSeconds;
        while (Time.time <= timeToStart)
        {
            float remainingTime = timeToStart - Time.time;
            int remainingTimeInt = Mathf.FloorToInt(remainingTime);
            countdownText.text = (remainingTimeInt+1).ToString();
            if(previousRemaininTimeInt != remainingTimeInt)
            {
                hudAudioController.PlayCountdownAudio();
            }
            previousRemaininTimeInt = remainingTimeInt;

            float percent = remainingTime - remainingTimeInt;
            countdownText.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, percent);

            yield return null;
        }
        hudAudioController.PlayCountdownEndAudio();

        countdownText.gameObject.SetActive(false);

    }
}
