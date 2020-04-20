using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
	public Transform MainCam; //Main Camera
	public float MovementDistance=3.0f; //How many degrees out the camera is allowed to move
	public float movementRestriction = 0.9f;
	private Quaternion startAngle; //Original Position of Camera
	private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startAngle=MainCam.rotation;
        startPos = MainCam.position;
    }

    // Update is called once per frame
    void Update()
    {
    	var RealMouseX = Input.mousePosition.x; //Get Mouse Position based on left side of screen in pixels
    	var RealMouseY = Input.mousePosition.y; // Get Mouse position based on right side of screen in pixels
    	var MouseOffsetX = Mathf.Clamp((RealMouseX-(Screen.width/2))/Screen.width,-1,1); //Convert Mouse X to value between -1 and 1 where 0 is the center.
    	var MouseOffsetY = Mathf.Clamp((RealMouseY-(Screen.height/2))/Screen.height,-1,1); //Convert Mouse Y to value between -1 and 1 where 0 is the center.
        MainCam.rotation = Quaternion.Slerp(MainCam.rotation, startAngle*Quaternion.Euler( -MouseOffsetY*MovementDistance, MouseOffsetX*MovementDistance, 0.0f),0.2f); // Set the rotation to the relative offset.
        MainCam.position = Vector3.Lerp(MainCam.position, startPos + new Vector3(-MouseOffsetX*movementRestriction, -MouseOffsetY*movementRestriction,0.0f),0.2f);
    }
}
