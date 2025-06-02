using UnityEngine;

public class CricketJumpPickup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerMovement>().hasCricketPower = true;
        print ("CricketJump Unlocked!");
    Destroy(gameObject);
    }

}
