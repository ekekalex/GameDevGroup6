using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Data;

public class HeartHealth : MonoBehaviour
{
    public int maxHearts = 3;
    public int healthPerHeart = 2; // full heart = 2HP 
    private int currentHeath;

    public Sprite fullHeart;  //full heart 
    public Sprite halffHeart; // half heart
    public Sprite emptyHeart;  //empty
    public Image[] hearts;  // drag the hearts image here!
    public GameObject gameOverUI;

    private void Start()
    {
        currentHeath = maxHearts * healthPerHeart;
        UpdateHearts();
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHeath -= amount;
        currentHeath = Mathf.Clamp(currentHeath, 0, maxHearts * healthPerHeart);
        UpdateHearts();
        if (currentHeath <= 0)
        {
            GameOver();
        }
    }
    public void Heal(int amount)
    {
        currentHeath += amount;
        currentHeath = Mathf.Clamp(currentHeath, 0, maxHearts * healthPerHeart);
        UpdateHearts();
    }
    private void UpdateHearts()
    {
        int health = currentHeath;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (health >= 2)
            {
                hearts[i].sprite = fullHeart;
                health -= 2;
            }
            else if (health == 1)
            {
                hearts[i].sprite = halffHeart;
                health -= 1;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
    private void GameOver()
    {
        Time.timeScale = 0f;
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) // disable playermovement if HP all gone
        {
            var movement = player.GetComponent<PlayerMovement>();
            if (movement != null)
            {
                movement.enabled = false;
            }
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        //assign GameOver panel
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}