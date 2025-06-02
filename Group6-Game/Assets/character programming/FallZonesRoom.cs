using UnityEngine;
public class RoomFallZone : MonoBehaviour
{
    public Transform respawnPoint;
    public int damageOnFall = 1;
    private void OTriggerEnter(Collider other)
    {
        HeartHealth health = other.GetComponent<HeartHealth>();
        if (health != null)
        {
            health.TakeDamage(damageOnFall);
            other.transform.position = respawnPoint.position;
        }
    }
}