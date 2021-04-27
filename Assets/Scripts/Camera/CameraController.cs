using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private Transform cam;
    [SerializeField] private float cameraMovementSpeed = 10;
    [SerializeField] private float cameraZoomSpeed = 10;
    [SerializeField] private float edgeTolerance = 0.1f;
    [SerializeField] private float rotationAngle = 45.0f;
    [SerializeField] private float minHorizontalLimit = -25.0f;
    [SerializeField] private float maxHorizontalLimit = 25.0f;
    [SerializeField] private float minVerticalLimit = -25.0f;
    [SerializeField] private float maxVerticalLimit = 25.0f;
    
    private float mouseAngle;
    private Vector2 mousePosition;
    
    public void Update() {
        //Read input and normalize with origin at center
        mousePosition.x = Input.mousePosition.x - Screen.width*0.5f;
        mousePosition.y = Input.mousePosition.y - Screen.height*0.5f;
        mouseAngle = Mathf.Rad2Deg * Mathf.Atan2(mousePosition.y,mousePosition.x);
        if(mouseAngle < 0) {
            mouseAngle += 360;
        }
        
        HandleMovementInput();
        HandleZoomInput();
    }

    void HandleZoomInput() {
        if(Input.mouseScrollDelta.y != 0) {

        }
    }

    void HandleMovementInput() {
        if ( cam.transform.position.x <= maxHorizontalLimit && cam.transform.position.x >= minHorizontalLimit && cam.transform.position.z <= maxVerticalLimit && cam.transform.position.z >= minVerticalLimit) {
            if ( ( (Input.mousePosition.y > Screen.height * (1 - edgeTolerance) || Input.mousePosition.y < Screen.height * edgeTolerance) || 
                ( Input.mousePosition.x > Screen.width * (1 - edgeTolerance) || Input.mousePosition.x < Screen.width * edgeTolerance) ) ) {
                cam.transform.Translate(Vector3.right * Time.deltaTime * cameraMovementSpeed * Mathf.Cos(Mathf.Deg2Rad * mouseAngle));
                cam.transform.Translate(Vector3.forward * Time.deltaTime * cameraMovementSpeed * Mathf.Sin(Mathf.Deg2Rad * mouseAngle));
            }     
        }
        if(cam.transform.position.x > maxHorizontalLimit) {
                cam.transform.position = new Vector3(maxHorizontalLimit, cam.transform.position.y, cam.transform.position.z);
            }
            if(cam.transform.position.x < minHorizontalLimit) {
                cam.transform.position = new Vector3(minHorizontalLimit, cam.transform.position.y, cam.transform.position.z);
            }
            if(cam.transform.position.z > maxVerticalLimit) {
                cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y,maxVerticalLimit);
            }
            if(cam.transform.position.z < minVerticalLimit) {
                cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y,minVerticalLimit);
        }
    }
}
