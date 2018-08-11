using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class WaveSpawner : MonoBehaviour {
    
    [SerializeField]
    private float waitTime;
    private GameObject[] enemies;
    private GameObject[] minionSpawnPoints;
    private bool spawning;
    private bool didRestart;
    private int wave = 0;
    private int rider = 0;
    [SerializeField]
    private GameObject waveCounter;
    private Text waveCounterText;
    [SerializeField]
    private Transform riderSpawn;
    public static bool riderWave;
    private GameObject currentRider;
    [SerializeField]
    private GameObject[] riderPrefabs;

    [Header("Conquest")]
    [SerializeField]
    private GameObject whiteSwordMinions;
    [SerializeField]
    private GameObject whiteArcherMinions;
    [SerializeField]
    private GameObject whiteCavalryMinions;
    [SerializeField]
    private GameObject whiteFlyingMinions;

    [Header("War")]
    [SerializeField]
    private GameObject redSwordMinions;
    [SerializeField]
    private GameObject redArcherMinions;
    [SerializeField]
    private GameObject redCavalryMinions;
    [SerializeField]
    private GameObject redFlyingMinions;

    [Header("Famine")]
    [SerializeField]
    private GameObject blackSwordMinions;
    [SerializeField]
    private GameObject blackArcherMinions;
    [SerializeField]
    private GameObject blackCavalryMinions;
    [SerializeField]
    private GameObject blackFlyingMinions;

    [Header("Death")]
    [SerializeField]
    private GameObject paleSwordMinions;
    [SerializeField]
    private GameObject paleArcherMinions;
    [SerializeField]
    private GameObject paleCavalryMinions;
    [SerializeField]
    private GameObject paleFlyingMinions;
    [SerializeField]
    private GameObject paleRevivedMinions;

    private void Start()
    {
        spawning = true;
        riderWave = false;
        waveCounterText = waveCounter.GetComponent<Text>();
        minionSpawnPoints = GameObject.FindGameObjectsWithTag("MinionWaveSpawnPoint");
    }

    void Update ()
    {
        waveCounterText.text = "Current Wave: " + wave.ToString();
        waitTime += Time.deltaTime;
        enemies = GameObject.FindGameObjectsWithTag("EnemyUnits");
        ResetSpawning();

        if (spawning == true)
        {
            SpawnWave();
            spawning = false;
        }
	}

    void ResetSpawning()
    {
        if (enemies.Length <= 0)
        {
            spawning = true;
        }
    }

    void SpawnWave()
    {
        if (wave == 0)
        {
            RiderPassageRestart();
            if (waitTime >= 9f)
            {
                Wave0();
                didRestart = false;
            }
        } else if (wave == 1)
        {
            Wave1();
        } else if (wave == 2)
        {
            Wave2();
        } else if (wave == 3)
        {
            Wave3();
        } else if (wave == 4)
        {
            Wave4();
        } else if (wave == 5)
        {
            Wave5();
        }
        else if (wave == 6)
        {
            Wave6();
        }
        else if (wave == 7)
        {
            Wave7();
        }
        else if (wave == 8)
        {
            Wave8();
        }
        else if (wave == 9)
        {
            Wave9();
        }
    }

    void RiderPassageRestart()
    {
        if (!didRestart)
        {
            currentRider = Instantiate(riderPrefabs[rider], riderSpawn.position, Quaternion.Euler(0, 180, 0));
            RiderPassage.Restart(rider);
            rider++;
            didRestart = true;
        }
    }

    void Spawn(GameObject minion, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(minion, minionSpawnPoints[Random.Range(0, minionSpawnPoints.Length)].transform.position, Quaternion.Euler(0, 180, 0));
        }
    }

    void Wave0()
    {
        Spawn(whiteSwordMinions, 2);
        wave++;
    }

    void Wave1()
    {
        Spawn(whiteSwordMinions, 3);
        wave++;      
    }

    void Wave2()
    {
        Spawn(whiteSwordMinions, 4);
        Spawn(whiteArcherMinions, 2);
        wave++;     
    }

    void Wave3()
    {
        Spawn(whiteSwordMinions, 5);
        Spawn(whiteArcherMinions, 3);
        Spawn(whiteCavalryMinions, 2);
        wave++;      
    }

    void Wave4()
    {
        Spawn(whiteSwordMinions, 5);
        Spawn(whiteArcherMinions, 5);
        Spawn(whiteCavalryMinions, 5);
        wave++;     
    }

    void Wave5()
    {
        Spawn(whiteSwordMinions, 10);
        Spawn(whiteArcherMinions, 5);
        Spawn(whiteCavalryMinions, 5);
        Spawn(whiteFlyingMinions, 5);
        wave++;
    }

    void Wave6()
    {
        Spawn(whiteSwordMinions, 10);
        Spawn(whiteArcherMinions, 8);
        Spawn(whiteCavalryMinions, 8);
        Spawn(whiteFlyingMinions, 5);
        wave++;
    }

    void Wave7()
    {
        Spawn(whiteSwordMinions, 15);
        Spawn(whiteArcherMinions, 10);
        Spawn(whiteCavalryMinions, 10);
        Spawn(whiteFlyingMinions, 8);
        wave++;
    }

    void Wave8()
    {
        Spawn(whiteSwordMinions, 10);
        Spawn(whiteArcherMinions, 5);
        Spawn(whiteCavalryMinions, 10);
        Spawn(whiteFlyingMinions, 15);
        wave++;
    }

    void Wave9()
    {
        currentRider.GetComponent<NavMeshObstacle>().enabled = false;
        currentRider.GetComponent<NavMeshAgent>().enabled = true;
        currentRider.GetComponent<Rider>().enabled = true;
        currentRider.GetComponent<Health>().enabled = true;
        currentRider.GetComponent<WhiteRider>().enabled = true;
        IsRiderWave();
        Spawn(whiteSwordMinions, 20);
        Spawn(whiteArcherMinions, 20);
        Spawn(whiteCavalryMinions, 20);
        Spawn(whiteFlyingMinions, 20);
        wave++;
    }

    private static void IsRiderWave()
    {
        //Wave 9 & 10, 19 & 20, 29 & 30, 39 & 40
        riderWave = !riderWave;
    }

}
