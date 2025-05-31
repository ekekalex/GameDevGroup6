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
        int HP = currentHeath;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (HP >= 2)
            {
                hearts[i].sprite = fullHeart;
                HP -= 2;
            }
            else if (HP == 1)
            {
                hearts[i].sprite = halffHeart;
                HP -= 1;
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
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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