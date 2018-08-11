using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

    [Header("General Serializables")]
    [SerializeField]
    private GameManager unitCreation;
    [SerializeField]
    private Text tip;
    [SerializeField]
    private GameObject rootPanel;
    [SerializeField]
    private GameObject areYouSurePanel;
    [SerializeField]
    private GameObject shopPanel;
    [SerializeField]
    private GameObject gameControlsPanel;

    [Header("General Buttons")]
    [SerializeField]
    private Button exitGameMenu;
    [SerializeField]
    private Button gameControls;
    [SerializeField]
    private Button shop;
    [SerializeField]
    private Button newGame;
    [SerializeField]
    private Button exitGame;

    [Header("Are You Sure?")]
    [SerializeField]
    private Text areYouSure;
    [SerializeField]
    private Button yes;
    [SerializeField]
    private Button no;

    [Header("General Variables")]
    private float waitTime;
    private static bool notCutScene = false;

    private void Start()
    {
        ShowOrHidePanel(0);
        gameControls.onClick.AddListener(GameControls);
        shop.onClick.AddListener(Shop);
        exitGameMenu.onClick.AddListener(ExitGameMenu);
        no.onClick.AddListener(No);
        newGame.onClick.AddListener(NewGame);
        exitGame.onClick.AddListener(ExitGame);
        tip.canvasRenderer.SetAlpha(0.0f);
        waitTime = 29f;
    }

    private void Update()
    {
        if (notCutScene)
        {
            waitTime += Time.deltaTime;
            if (waitTime >= 30f)
            {
                TipFadeIn();
            }
            TipFadeOut();
        }
        
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            rootPanel.SetActive(true);
            unitCreation.GetComponent<UnitCreation>().enabled = false;
            Time.timeScale = 0.0f;
        }
    }

    void TipFadeOut ()
    {
        if (waitTime >= 3f)
        {
            tip.CrossFadeAlpha(0f, 1f, false);
        }
    }

    void TipFadeIn ()
    {
        tip.GetComponent<Text>().canvasRenderer.SetAlpha(0.0f);
        tip.CrossFadeAlpha(1f, 1f, false);
        waitTime = 0f;
    }

    void GameControls()
    {
        ShowOrHidePanel(0);
    }

    void Shop()
    {
        ShowOrHidePanel(1);
    }

    void ExitGameMenu()
    {
        rootPanel.SetActive(false);
        unitCreation.GetComponent<UnitCreation>().enabled = true;
        Time.timeScale = 1.0f;
    }

    void NewGame()
    {
        ShowOrHidePanel(2);
        areYouSure.text = "New Game?";
        yes.onClick.RemoveAllListeners();
        yes.onClick.AddListener(YesNew);
    }

    void ExitGame()
    {
        ShowOrHidePanel(2);
        areYouSure.text = "Quit Game?";
        yes.onClick.RemoveAllListeners();
        yes.onClick.AddListener(YesQuit);
    }

    void ShowOrHidePanel(int panel)
    {
        if (panel == 0)
        {
            gameControlsPanel.SetActive(true);
        } else
        {
            gameControlsPanel.SetActive(false);
        }

        if (panel == 1)
        {
            shopPanel.SetActive(true);
        } else
        {
            shopPanel.SetActive(false);
        }

        if (panel == 2)
        {
            areYouSurePanel.SetActive(true);
        } else
        {
            areYouSurePanel.SetActive(false);
        }
    }

    void YesNew()
    {
        SceneManager.LoadScene(0);
        Debug.Log("GameRestarting");
    }

    void YesQuit()
    {
        Application.Quit();
        Debug.Log("GameExiting");
    }
    
    void No()
    {
        ShowOrHidePanel(0);
    }

    public static void NotCutScene()
    {
        notCutScene = true;
    }

}
