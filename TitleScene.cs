using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour {

    [SerializeField]
    private Text title;
    private float waitTime;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1.0f;
        InvokeRepeating("FadeOut", 0.0f, 0.5f);
        Cursor.lockState = CursorLockMode.Confined;
        title.canvasRenderer.SetAlpha(0.0f);
        title.CrossFadeAlpha(1.0f, 3.0f, true);
    }
	
	// Update is called once per frame
	void Update () {
        waitTime += Time.deltaTime;
        if (waitTime >= 7.5f)
        {
            SceneManager.LoadScene(1);
        }
	}

    void FadeOut()
    {
        if (waitTime >= 4f)
        {
            title.CrossFadeAlpha(0.0f, 3.0f, true);
            CancelInvoke();
        }
    }

}
