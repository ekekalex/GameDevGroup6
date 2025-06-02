using Unity.Mathematics;
using UnityEngine;
public class RoomFallZone : MonoBehaviour
{
    public Transform respawnPoint;
    public int damageOnFall = 1;
    public GameObject playerPrefab;

    private void OnTriggerEnter(Collider other)
    {
        print("respawn trigger hit: " + other.name);
        HeartHealth health = other.GetComponent<HeartHealth>();
        if (health != null)
        {
            health.TakeDamage(damageOnFall);
            CharacterController controller = GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false;
                //other.transform.position = respawnPoint.position;
                controller.enabled = true;
            }
            else
            {
                //other.transform.position = respawnPoint.position;
            }
        }
        GameObject NewPlayer = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        NewPlayer.GetComponent<HeartHealth>().currentHeath = other.GetComponent<HeartHealth>().currentHeath;
        NewPlayer.GetComponent<PlayerMovement>().hasCricketPower = other.GetComponent<PlayerMovement>().hasCricketPower;
        Destroy(other.gameObject);

    }
}