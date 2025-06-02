using UnityEngine;
public class RoomFallZone : MonoBehaviour
{
    public Transform respawnPoint;
    public int damageOnFall = 1;
    private void OnTriggerEnter(Collider other)
    {
        HeartHealth health = other.GetComponent<HeartHealth>();
        if (health != null)
        {
            health.TakeDamage(damageOnFall);
            CharacterController controller = GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false;
                transform.position = respawnPoint.position;
                controller.enabled = true;
            }
            else
            {
                other.transform.position = respawnPoint.position;
            }
        }
    }
}