//death zone triggering 
using UnityEngine;
public class DeathZone : MonoBehaviour
{
    private void OiggerEnter(Collider other)
    {
        HeartHealth heath = other.GetComponent<HeartHealth>();
        if (heath != null)
            heath.Respawn();
    }
}