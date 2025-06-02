using System;
using System.Collections.Generic;
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
    public Sprite halfHeart; // half heart
    public Sprite emptyHeart;  //empty
    public GameObject heartPrefab;
    public Transform heartsContainer;
    public GameObject gameOverUI;
    private List<Image> heartImages = new List<Image>();


    private void Start()
    {
        currentHeath = maxHearts * healthPerHeart;
        UpdateHearts();
        GenerateHearts();
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }
    private void GenerateHearts()
    {
        foreach (Transform child in heartsContainer)
        {
            Destroy(child.gameObject);
        }

        heartImages.Clear();

        for (int i = 0; i < maxHearts; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartsContainer);
            Image img = newHeart.GetComponent<Image>();
            heartImages.Add(img);
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
        for (int i = 0; i < heartImages.Count; i++)
        {
            if (HP >= 2)
            {
                heartImages[i].sprite = fullHeart;
                HP -= 2;
            }
            else if (HP == 1)
            {
                heartImages[i].sprite = halfHeart;
                HP -= 1;
            }
            else
            {
                heartImages[i].sprite = emptyHeart;
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(1);
        }
    }
}