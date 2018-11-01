using UnityEngine;
using System.Collections;

public class EvasiveManeuver : MonoBehaviour
{
    public float speedo;
    public float dodge;
    public float smoothing;
    public float tilt;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public Boundary boundary;
    private Transform playerTransform;
    private GameObject PlayerPos;

    private float currentSpeed;
    private float targetManeuver;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerPos = GameObject.FindWithTag("Player"); ;
        playerTransform = GameObject.FindWithTag("Player").transform;
        currentSpeed = rb.velocity.x;
        StartCoroutine(Evade());
    }
    void Update()
    {
        Debug.Log(currentSpeed, playerTransform);
    }
    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        playerTransform = GameObject.FindWithTag("Player").transform;
        if(playerTransform == null)
        {
            Debug.Log(playerTransform);
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.y);
        }
        while (true)
        {
            //targetManeuver = Random.Range(-12, 12);
            targetManeuver = playerTransform.position.y;
            Debug.Log(targetManeuver);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            //targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

    void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.y, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(currentSpeed, newManeuver, 0.0f);
        rb.position = new Vector3
       (
           Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),Mathf.Clamp(rb.position.y, boundary.yMin, boundary.xMax), 0.0f);

        rb.rotation = Quaternion.Euler(rb.velocity.y * tilt, 0.0f, 0.0f);
    }
}