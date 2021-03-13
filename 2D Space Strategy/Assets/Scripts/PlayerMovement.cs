using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    float moveSpeed = 20;

    float zoomSpeed = 2;

    float currentZoom = 20;

    Camera mainCamera;

	// Use this for initialization
	void Start () {

        mainCamera = FindObjectOfType<Camera>();

    }
	
	// Update is called once per frame
	void Update () {

        Movement();
        Zoom();

	}

    void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-moveSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -moveSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }

    }

    void Zoom()
    {

        if (Input.mouseScrollDelta.y > 0)
        {
            currentZoom -= zoomSpeed;
            if (currentZoom <= 0)
            {
                currentZoom = 1;
            }

        }
        if (Input.mouseScrollDelta.y < 0)
        {
            currentZoom += zoomSpeed;
        }

        mainCamera.orthographicSize = currentZoom;

    }
}
