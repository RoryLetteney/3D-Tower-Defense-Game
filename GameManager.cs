using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Text killCounter;
    [SerializeField]
    private Text casualtyCounter;
    [SerializeField]
    private Text remainingPopulation;
    private static int kills = 0;
    private static int casualties = 0;
    private static int population = 10000;
        
    private void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        killCounter.text = "Kills: " + kills.ToString();
        casualtyCounter.text = "Casualties: " + casualties.ToString();
        remainingPopulation.text = "Population: " + population.ToString();
    }

    public static void ReceiveKills()
    {
        kills++;
    }
    
    public static void ReceiveCasualties(int amount)
    {
        casualties += amount;
    }

    public static void DecreasePopulation(int amount)
    {
        population -= amount;
    }

}
