using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiderPassage : MonoBehaviour {

    private static GameObject gameManager;
    private static GameObject gameMenu;
    private static Camera playerCam;
    private static MonoBehaviour playerCamController;
    private static MonoBehaviour playerCamLerp;
    private Image banner1;
    private Image banner2;
    private static GameObject[] banners;
    private bool gameStart = true;

    private static Text txt;
    private static float waitTime;
    private static GameObject hud;
    private static CanvasGroup hudCG;
    private float currentAlphaTxt;
    private float currentAlphaB1;
    private float currentAlphaB2;
    private static string[] riderPassage = {
        "I looked, and behold, a white horse. He who sat on it had a bow; and a crown was given to him, and he went out conquering and to conquer.",
        "I looked, and behold, a red horse. And it was granted to the one who sat on it to take peace from the earth, and that people should kill one another; and there was given to him a great sword.",
        "I looked, and behold, a black horse, and he who sat on it had a pair of scales in his hand. And I heard a voice in the midst of the four living creatures saying, \"A quart of wheat for a denarius, and three quarts of barley for a denarius; and do not harm the oil and the wine.\"",
        "I looked, and behold, a pale horse. And the name of him who sat on it was Death, and Hades followed with him. And power was given to them over a fourth of the earth, to kill with sword, with hunger, with death, and by the beasts of the earth."
    };

	// Use this for initialization
	void Start () {
        waitTime = 0f;
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameMenu = GameObject.FindGameObjectWithTag("GameMenu");
        playerCam = Camera.main;
        playerCamController = playerCam.gameObject.GetComponent<CameraControl>();
        playerCamLerp = playerCam.gameObject.GetComponent<RiderPassageLerp>();
        txt = GetComponent<Text>();
        hud = GameObject.FindGameObjectWithTag("PlayerHUD");
        banners = GameObject.FindGameObjectsWithTag("RiderPassageBanner");
        banner1 = banners[0].GetComponent<Image>();
        banner2 = banners[1].GetComponent<Image>();
        hudCG = hud.GetComponent<CanvasGroup>();
        InvokeRepeating("FadeOut", 0f, 0.5f);
        txt.canvasRenderer.SetAlpha(0.0f);
    }

    private void Update()
    {
        currentAlphaTxt = txt.canvasRenderer.GetAlpha();
        currentAlphaB1 = banners[0].GetComponent<Image>().canvasRenderer.GetAlpha();
        currentAlphaB2 = banners[1].GetComponent<Image>().canvasRenderer.GetAlpha();
        waitTime += Time.deltaTime;
        if (gameStart)
        {
            if (waitTime >= 2.0f)
            {
                txt.CrossFadeAlpha(1.0f, 2f, false);
                gameStart = false;
            }
        }
        if (waitTime >= 10f)
        {
            hudCG.alpha += Time.deltaTime;
        }        
    }

    private void FadeOut()
    {        
        if (waitTime >= 9f)
        {
            gameMenu.GetComponent<GameMenu>().enabled = true;
            gameManager.GetComponent<UnitCreation>().enabled = true;
            playerCamController.enabled = true;
            playerCamLerp.enabled = false;
            GameMenu.NotCutScene();
            if (currentAlphaTxt == 1f)
            {
                txt.CrossFadeAlpha(0f, 1f, false);
            }
            if (currentAlphaB1 == 1f)
            {
                banner1.CrossFadeAlpha(0f, 1f, false);
            }
            if (currentAlphaB2 == 1f)
            {
                banner2.CrossFadeAlpha(0f, 1f, false);
            }
        }
    }

    public static void Restart(int rider)
    {
        txt.text = riderPassage[rider];
        if (waitTime >= 2f)
        {
            txt.canvasRenderer.SetAlpha(0.0f);
            txt.CrossFadeAlpha(1.0f, 2f, false);
        }        
        foreach (GameObject banner in banners)
        {
            banner.GetComponent<Image>().canvasRenderer.SetAlpha(1.0f);
        }
        gameMenu.GetComponent<GameMenu>().enabled = false;
        gameManager.GetComponent<UnitCreation>().enabled = false;
        playerCamController.enabled = false;
        playerCamLerp.enabled = true;
        RiderPassageLerp.NewStartTime();
        hudCG.alpha = 0.0f;
        waitTime = 0f;
    }

}
