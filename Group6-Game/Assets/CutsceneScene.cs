using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FinalCutsceneTrigger : MonoBehaviour
{
    public Camera mainCamera;
    public Camera finalCutsceneCamera;
    public GameObject menuUI;
    public float cutsceneDuration = 5f;
    public Animator cutsceneAnimator; // Animator for the cutscene animation
    public Animator[] additionalAnimators;
    public GameObject[] objectsToActivate;

    private bool triggered = false;

    private void Start()
    {
        if (menuUI != null)
            menuUI.SetActive(false); // Ensure menu is hidden at start
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(PlayFinalCutscene());
        }
    }

    private IEnumerator PlayFinalCutscene()
    {
        // Switch cameras
        mainCamera.enabled = false;
        finalCutsceneCamera.enabled = true;

        // Play main animation
        if (cutsceneAnimator != null)
            cutsceneAnimator.SetTrigger("PlayCutscene");

        // Trigger additional animations
        foreach (Animator anim in additionalAnimators)
        {
            if (anim != null)
                anim.SetTrigger("PlayCutscene");
        }

        // Activate objects
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        // Wait for cutscene to finish
        yield return new WaitForSeconds(cutsceneDuration);

        // Hide cutscene camera
        finalCutsceneCamera.enabled = true;

        // Show menu
        menuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    // Button functions
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game quit.");
    }
}
