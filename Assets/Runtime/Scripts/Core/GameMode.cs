using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    [SerializeField] private float reloadGame;

    public void OnGameOver()
    {
        StartCoroutine(ReloadGameCoroutine());
    }

    private IEnumerator ReloadGameCoroutine()
    {
        yield return new WaitForSeconds(reloadGame);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
