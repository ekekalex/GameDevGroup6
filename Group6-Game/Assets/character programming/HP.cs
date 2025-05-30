using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthStatus : MonoBehaviour
{
    public int maxHP = 6;
    private int currentHP;

    public GameObject gameOverScreen;
    private void Start()
    {
        currentHP = maxHP;
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }
    }
    public void Damage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
//