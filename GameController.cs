using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public AudioSource SpeedTalk;
    public AudioSource missleTalk;
    public AudioSource lasersound;
    public AudioSource laserTalk;
    public GameObject player;
    private PlayerController playerController;
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public Vector3 playerSpawn;
    public Vector3[] SpawnEnemy;
    public float[] spawnWait;
    public float startWait;
    public float[] waveWait;
    public Text scoreText;
    public Text Lifeinfo;
    public Text restartText;
    public Text gameOverText;
    public Text Speedup;
    public Text MissileText;
    public Text LaserText;
    public int hazardCount;
    public int lifes;
    private int pointstolife;
    public int requiredpointstolife;
    public int upSpeed;
    public int upMissile;
    public int score;
    public int finalscore;
    private bool gameOver;
    private bool restart;

    void Start()
    {
        gameOver = false; // Sets Gameover state to false on first frame
        restart = false; // Same as above but restart state
        restartText.text = "";
        gameOverText.text = "";
        Lifeinfo.text = "Lifes: " + lifes;
        score = 0;
        StartCoroutine (updatescore()); //Calls Updatescore fuction
        StartCoroutine (SpawnWaves()); //Calls Spawnwave fuction
    }
    void Update()
    {
        if (restart)//check for restart state
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Scene loadedLevel = SceneManager.GetActiveScene();//gets current level so you restart in same level
                SceneManager.LoadScene(loadedLevel.buildIndex);
            }
        }
        GameObject gameControllerObject = GameObject.FindWithTag("Player");
        playerController = gameControllerObject.GetComponent<PlayerController>();
        if (playerController.UpGradePoint == 1)//these if startments highlight current upgrade
        {
            Speedup.color = Color.red;
        }
        else
        {
            Speedup.color = Color.white;
        }
        if(playerController.UpGradePoint == 2)
        {
            MissileText.color = Color.red;
        }
        else
        {
            MissileText.color = Color.white;
        }
        if(playerController.UpGradePoint == 3)
        {
            LaserText.color = Color.red;
        }
        else
        {
            LaserText.color = Color.white;
        }
        playerController = gameControllerObject.GetComponent<PlayerController>();
        if (Input.GetKeyDown(KeyCode.E));//not an error
        {
            UpGradesystem();
        }

    }
    //Giant Cheese Taco!
    IEnumerator SpawnWaves()// handles spawning of enemy npc's
    {
        Scene curentScene = SceneManager.GetActiveScene();
        string SceneName = curentScene.name;
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            yield return new WaitForSeconds(waveWait[0]);
            if (SceneName == "Level01")
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    GameObject hazard = hazards[0];
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, SpawnEnemy[0], spawnRotation);
                    yield return new WaitForSeconds(spawnWait[0]);

                }
                yield return new WaitForSeconds(waveWait[1]);
                for (int i = 0; i < hazardCount; i++)
                {
                    GameObject hazard = hazards[0];
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, SpawnEnemy[1], spawnRotation);
                    yield return new WaitForSeconds(spawnWait[0]);
                }
            }
            yield return new WaitForSeconds(waveWait[2]);

            if (gameOver)// checks for game over state and enables reset state
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }

        }
       
    }

    public void ascore(int addscore)//score keeper and builder
    {
        score += addscore;
        StartCoroutine(updatescore());
    }
    IEnumerator updatescore()//update's the scrore on screen
    {
            yield return new WaitForSeconds(0.2f);
            scoreText.text = "Score: " + score;
            pointstolife = score;
            if (requiredpointstolife <= pointstolife)//checks curent score to see if you have enuff points to get an extra live
        {
            lifes = lifes + 1;
            requiredpointstolife = requiredpointstolife * 2;// dubles required scroe for extra life so you can acumate lives
            scoreText.text = "Score: " + score;
        }


    }
    public void GameOver()//gameover fuction hadles gameover state and respawning
    {
        if (lifes <= 0)//life check before Calling gameover
        {
            gameOverText.text = "Game Over!";
            gameOver = true;
        }
        else// if has lifes respawns player at specified xyz loacion
        {
            Quaternion spawnRotation2 = Quaternion.identity;
            lifes = lifes - 1;
            Lifeinfo.text = "Lifes: " + lifes;
            Instantiate(player, playerSpawn, spawnRotation2);
        }
        
    }
    public void UpGradesystem()//handles upgrages aqired ingame make's checks for requied points and 
    {
        if(Input.GetKeyDown (KeyCode.E) && playerController.UpGradePoint == 1)//applys speed up upgrade
        {
            playerController.speed = playerController.speed + 5;
            SpeedTalk.Play();
            playerController.UpGradePoint = playerController.UpGradePoint - upSpeed;
        }
        if (Input.GetKeyDown(KeyCode.E) && playerController.UpGradePoint == 2)//applys missle upgrade
        {
            missleTalk.Play();
            playerController.UpGradePoint = playerController.UpGradePoint - upMissile;
            playerController.hasMissle = true;
        }
        if (Input.GetKeyDown(KeyCode.E) && playerController.UpGradePoint == 3)// applys laser upgrade
        {
            playerController.UpGradePoint = playerController.UpGradePoint - 3;
            laserTalk.Play();
            playerController.hasLaser = true;
        }
        if (Input.GetKeyDown(KeyCode.E) && playerController.UpGradePoint == 4)
        {
            playerController.UpGradePoint = playerController.UpGradePoint - 4;
            playerController.hasOption = true;
        }
       
    }
    public void laseraudio()// switch shot audio for laser audio
    {
        lasersound.Play();
    }
}