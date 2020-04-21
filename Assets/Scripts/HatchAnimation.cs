using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchAnimation : MonoBehaviour
{
	public Transform MainCamera;
	public Transform TopHalf;
	public Transform BottomHalf;
	public Transform DirLight;
	public Transform WhiteGrow;
	private Light Lighter;
	private float timer;
    // Start is called before the first frame update
    void Start()
    {
        Lighter=DirLight.GetComponent<Light>();
        MainCamera.position = new Vector3(0f,-0.3f,-7.97f);
        TopHalf.position = new Vector3();
        BottomHalf.position = new Vector3();
        Lighter.intensity=0.0f;
        WhiteGrow.localScale = new Vector3(0f,0f,0f);
    }

    // Update is called once per frame
    void Update()
    {
    	timer+=1*Time.deltaTime;
    	if (timer<2) {
	        MainCamera.position = new Vector3(0f,-0.3f,-7.97f);
	        TopHalf.position = new Vector3(0f,-0.71f,-3.3f);
	        BottomHalf.position = new Vector3(0f,-0.71f,-3.3f);
	        Lighter.intensity=0.0f;
    	}
    	if (timer>2) {
	        MainCamera.position =Vector3.Lerp(MainCamera.position, new Vector3(0f,-0.3f,-37.39f),0.1f);;
	        TopHalf.position = Vector3.Lerp(TopHalf.position,new Vector3(0f,5.65f,-3.3f),0.1f);
	        BottomHalf.position = Vector3.Lerp(BottomHalf.position,new Vector3(0f,-5.48f,-3.3f),0.1f);
	        Lighter.intensity=Mathf.Lerp(Lighter.intensity,160.0f,0.1f);
    	}
    	if (timer>3) {
        	WhiteGrow.localScale =Vector3.Lerp(WhiteGrow.localScale, new Vector3(100f,100f,100f),0.01f);
    	}
        
    }
}
