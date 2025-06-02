//death zone triggering 
using UnityEngine;
public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        HeartHealth heath = other.GetComponent<HeartHealth>();
        if (heath != null)
            heath.Respawn();
    }
}