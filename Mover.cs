using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    public float speed;
    public float startspeed;
    public bool isMissle;
    private Rigidbody rb;


    void Start()
    {
        if(isMissle == false)
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.right * speed;
        }
        if(isMissle == true)
        {
            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.right * Mathf.MoveTowards(startspeed, speed, 5);
            Debug.Log(rb.velocity);
        }
    }
}