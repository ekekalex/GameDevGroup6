using System;
using UnityEngine; //attach to each checkpoint
public class CheckPointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
            HeartHealth health = other.GetComponent<HeartHealth>();
            if (health != null)
            {
                health.SetCheckpoint(other.transform.position);
                Debug.Log("Last Saved Checkpoint was: " + transform.position);
            }
    }
}