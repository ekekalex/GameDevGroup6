using UnityEngine;

public class platform : MonoBehaviour
{
    public float speed; 
    public int startPoint;
    public Transform[] points;
    private int i;
    
    public GameObject antonio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = points[startPoint].position;
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, points[i].position) <0.02f)
        {
            i++;
            if(i == points.Length)
            { 
                i = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        antonio.transform.parent = gameObject.transform;
        print("parented");
    }
    void OnTriggerExit(Collider other)
    {
        antonio.transform.parent = null;
        print("unparented");
    }
}
