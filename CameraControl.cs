using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    [SerializeField]
    private float panSpeed = 20f;
    private float panBorderThickness = 10f;
    private float scrollSpeed = 800f;
    private float heightDiffForward;
    private float heightDiffBackward = 60f;
    private float heightDiffSide;
    [SerializeField]
    private Vector2 panLimit;
    private Vector2 scrollLimit = new Vector2(40f, 80f);
	
	// Update is called once per frame
	void Update () {
        heightDiffForward = transform.position.y - 10f;
        heightDiffSide = 50f - transform.position.y;
        CameraMovement();
	}

    void CameraMovement()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -panLimit.x - heightDiffSide, panLimit.x + heightDiffSide);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y - heightDiffBackward, panLimit.y - heightDiffForward);
        pos.y = Mathf.Clamp(pos.y, scrollLimit.x, scrollLimit.y);

        transform.position = pos;
    }
}
