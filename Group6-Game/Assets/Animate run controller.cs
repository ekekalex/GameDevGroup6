using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool isRunning = Input.GetKey(KeyCode.W);
        animator.SetBool("isRunning", isRunning);
    }
}
