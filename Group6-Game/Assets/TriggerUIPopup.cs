using UnityEngine;

public class TriggerUIPopup : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    private bool isPlayerInside = false;
    private bool isUIPanelVisible = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (uiPanel != null)
            {
                uiPanel.SetActive(true);
                isUIPanelVisible = true;
                isPlayerInside = true;
                Debug.Log("UI Panel shown. Press 'C' to close.");
            }
            else
            {
                Debug.LogWarning("uiPanel not assigned!");
            }
        }
    }

    private void Update()
    {
        if (isUIPanelVisible && Input.GetKeyDown(KeyCode.C))
        {
            uiPanel.SetActive(false);
            isUIPanelVisible = false;
            Debug.Log("UI Panel hidden.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }
}
