using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [SerializeField] float reloadGameDelay = 3;
    [SerializeField] PlayerController player;
    [SerializeField] PlayerAnimationController playerAnim;
    [SerializeField] MainHUD mainHud;
    [SerializeField] MusicPlayer musicPlayer;

    [SerializeField] int countdownSeconds = 5;

    void Awake()
    {
        player.enabled = false;
        musicPlayer.PlayStartMenuMusic();
    }

    public void OnGameOver()
    {
        StartCoroutine(ReloadGameCoroutine());
    }

    private IEnumerator ReloadGameCoroutine()
    {
        //esperar uma frame
        yield return new WaitForSeconds(reloadGameDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        musicPlayer.PlayMainTrackMusic();
        StartCoroutine(StartGameCor());
    }

    IEnumerator StartGameCor()
    {
        yield return StartCoroutine(mainHud.PlayStartCoroutine(countdownSeconds));
        playerAnim.GameStartAnim();
    }
}
