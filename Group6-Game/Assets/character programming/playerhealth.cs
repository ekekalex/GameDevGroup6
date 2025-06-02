using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthStatus : MonoBehaviour
{
    public int maxHP = 6;
    private int currentHP;

    public GameObject gameOverScreen;
    public GameObject gameOverUI;
    public delegate void OnHealthChanged(int currentHP);
    public static event OnHealthChanged OnHealthChangedEvent;
    private void Start()
    {
        currentHP = maxHP;
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
            TriggerHealthChanged();
        }
    }
    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        TriggerHealthChanged();

        if (currentHP <= 0)
        {
            GameOver();
        }
    }
    public void Heal(int amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        TriggerHealthChanged();
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
    private void TriggerHealthChanged()
    {
        OnHealthChangedEvent?.Invoke(currentHP);
    }
    public int GetCurrentHP()
    {
        return currentHP;
    }
}
//