using UnityEngine;

public class camFollowBall : MonoBehaviour
{
    public GameObject ballForm;
    public GameObject ballCam;

    // Update is called once per frame
    void Update()
    {
        ballCam.transform.position = ballForm.transform.position;
    }
}
