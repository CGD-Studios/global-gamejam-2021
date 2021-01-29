using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour {
    float camX;
    float camY;
    float camVert = 0f;
    float clampValue = 70f;
    [SerializeField]
    float mouseSens;

    Camera cam;

    void Awake() {
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
        camX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        camY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        camVert -= camY;
        camVert = Mathf.Clamp(camVert, -clampValue, clampValue);

        cam.transform.localRotation = Quaternion.Euler(camVert, 0f, 0f);
        transform.Rotate(Vector3.up * camX);
    }
}
