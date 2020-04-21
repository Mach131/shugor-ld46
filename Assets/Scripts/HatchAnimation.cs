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
	public Transform SFXer;
	public AudioClip Hatch;
	public AudioClip Boom;
	private AudioSource SFXPlayer;
	private Light Lighter;
	private float timer;
	private bool hasPlayed=false;
	public Transform Anim;
	public Transform Ending;
    // Start is called before the first frame update
    void Start()
    {
        Lighter=DirLight.GetComponent<Light>();
        MainCamera.position = new Vector3(0f,-0.3f,-7.97f);
        TopHalf.position = new Vector3();
        BottomHalf.position = new Vector3();
        SFXPlayer=SFXer.GetComponent<AudioSource>();
        Lighter.intensity=0.0f;
        WhiteGrow.localScale = new Vector3(0f,0f,0f);
        SFXPlayer.PlayOneShot(Hatch,0.7f);
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
	        if (hasPlayed==false) {
        		SFXPlayer.PlayOneShot(Boom,0.4f);
        		hasPlayed=true;
	        }
    	}
    	if (timer>3) {
        	WhiteGrow.localScale =Vector3.Lerp(WhiteGrow.localScale, new Vector3(100f,100f,100f),0.01f);
    	}
    	if (timer>6) {
        	Anim.gameObject.SetActive(false);
        	Ending.gameObject.SetActive(true);
    	}
        
        if(Input.GetKeyDown(KeyCode.Escape)) {
        	Application.Quit();
        }
    }
}
