using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGradePoint : MonoBehaviour
{
    public int points;
    private PlayerController playerController;
    // Use this for initialization
    void start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("Player");
        playerController = gameControllerObject.GetComponent<PlayerController>();
    }
    //   void OnTriggerEnter(Collider other) Was Caulsing Dubble Detection Glitch
    //{
    //     GameObject gameControllerObject = GameObject.FindWithTag("Player");
    //playerController = gameControllerObject.GetComponent<PlayerController>();
    //    if (other.CompareTag("Player"))
    //    {
    //        Destroy(gameObject);
    //playerController.Upgrade(points);
            
     //   }
   // }
//}
    void OnTriggerStay(Collider other)
    {
        GameObject gameControllerObject = GameObject.FindWithTag("Player");
        playerController = gameControllerObject.GetComponent<PlayerController>();
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            playerController.Upgrade(points);
            
        }
    }
}