using System;
using UnityEngine; //attach to each checkpoint
public class CheckPointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthStatus health = other.GetComponent<PlayerHealthStatus>();
            if (health != null)
            {
                health.SetCheckpoint(transform.position);
                Debug.Log("Last Saved Checkpoint was: " + transform.position);
            }
        }
    }
}