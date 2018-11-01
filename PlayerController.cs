using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}
public class PlayerController : MonoBehaviour
{

    public float speed;
    public bool hasMissle;
    public bool hasLaser;
    public bool hasOption;
    private Text Speedup;
    public float tilt;
    public int speedupValue;
    public int speedupmax;
    public Boundary boundary;
    private AudioSource sd;
    private Rigidbody rb;
    private GameController gameController;

    public int maxShots;
    public int maxShotsmissle;
    public GameObject shot;
    public GameObject laserShot;
    public GameObject missle;
    public Transform shotSpawn;
    public Transform Option1;
    public Transform Option2;
    public Transform Option3;
    public Transform Option4;
    public Transform shotSpawnmissle;
    public float fireRate;
    public float nextFiremissle;
    public float missleFireRate;
    private int shotcount;
    private int shotcountmiss;

    public int UpGradePoint;

    private float nextFire;
    private GameObject[] getCount;
    private GameObject[] getCountmiss;
    void Update()
    {

        sd = GetComponent<AudioSource>();
        getCount = GameObject.FindGameObjectsWithTag("playershot");
        shotcount = getCount.Length;
        getCountmiss = GameObject.FindGameObjectsWithTag("missle");
        shotcountmiss = getCountmiss.Length;
 
        if (Input.GetButton("Fire1") && Time.time > nextFire )
        {
            nextFire = Time.time + fireRate;
            if (shotcount < maxShots)
            {
                if (hasLaser == true)
                {

                    Instantiate(laserShot, shotSpawn.position, shotSpawn.rotation);
                    gameController.laseraudio();
                    maxShots = 10;
                }
                else if (shotcount < maxShots)
                {
                    Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                    sd.Play();
                }
            }
            if (hasOption == true)
            {
                if (hasLaser == true)
                {
                    Instantiate(laserShot, Option1.position, shotSpawn.rotation);
                    gameController.laseraudio();
                    maxShots = 20;
                }
                if (hasMissle == true && Time.time > nextFiremissle && shotcountmiss < maxShotsmissle)
                {
                    nextFiremissle = Time.time + missleFireRate;
                    Instantiate(missle, Option1.position, shotSpawn.rotation);
                    maxShotsmissle = 4;
                }
                if (hasMissle == true && hasLaser == true)
                {
                    if (Time.time > nextFiremissle && shotcountmiss < maxShotsmissle)
                    {
                        nextFiremissle = Time.time + missleFireRate;
                        Instantiate(missle, Option1.position, shotSpawn.rotation);
                        maxShotsmissle = 4;
                    }
                    Instantiate(laserShot, Option1.position, shotSpawn.rotation);
                    gameController.laseraudio();
                    maxShots = 20;
                }
                else
                {
                    Instantiate(shot, Option1.position, shotSpawn.rotation);
                    maxShots = 10;
                    sd.Play();
                }
            }
            
            if (hasMissle == true && Time.time > nextFiremissle && shotcountmiss < maxShotsmissle)
            {
                nextFiremissle = Time.time + missleFireRate;
                Instantiate(missle, shotSpawnmissle.position, shotSpawnmissle.rotation);
            }
        }
       
    }

    void start()
    {
        hasMissle = false;
        UpGradePoint = 0;
        sd = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody>();
        sd = GetComponent<AudioSource>();
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, -1.0f);
        

        rb.velocity = movement * speed;
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax), -1.0f);
        rb.rotation = Quaternion.Euler(0.0f, 90f, rb.velocity.y * tilt);
    }
    public void Upgrade(int point)
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        UpGradePoint = UpGradePoint + 1;
        Debug.Log(UpGradePoint);
    }
}