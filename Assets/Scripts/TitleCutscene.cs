using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCutscene : MonoBehaviour
{

	public float state=0.0f;
	public Transform FadeBackground;
	private Renderer BGRenderer;
	public Transform Tamagotchi;
	public Transform ShellEtc;
	public Transform ShugorLogo;
	private Color DarkColor;

	private AudioSource audioSource;
	public AudioClip ShugorJingle;
	public AudioClip Bleep;
	public Transform CrowdSounder;
	private AudioSource CrowdSounds;

	private float timer=0f;
	private Vector3 FBGPos;
	private bool hasPlayed;

	public string nextScene;

    // Start is called before the first frame update
    void Start()
    {
        BGRenderer=FadeBackground.GetComponent<Renderer>();
        DarkColor=BGRenderer.material.color;
        FBGPos = FadeBackground.position;
        audioSource = GetComponent<AudioSource>();
        CrowdSounds=CrowdSounder.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    	if(Input.GetButtonDown("Fire1") && state<3.0f) {
    		state+=1.0f;
    		audioSource.PlayOneShot(Bleep,0.7f);
    	}
    	if(Input.GetButtonDown("Fire2") && state<3.0f && state>0f) {
    		state-=1.0f;
    	}
        if(state==0.0f) {
        	BGRenderer.material.SetColor("_Color", DarkColor);
        	transform.position=new Vector3(-2.5f,0.0f,-3.9f);
        	transform.rotation = Quaternion.Euler(72.50101f,242.63f,0f);
        	transform.localScale = new Vector3(0f,0f,0f);
        	Tamagotchi.position = new Vector3(0f,-3f,10.87f);
        	Tamagotchi.rotation = Quaternion.Euler(116.628f,435.167f,68.529f);
        	Tamagotchi.localScale = new Vector3(0f,0f,0f);
        	timer=0f;
        	ShugorLogo.localScale=new Vector3(0f,0f,0f);
        	FadeBackground.position=FBGPos;
        	hasPlayed=false;
        	CrowdSounds.volume=0f;
        }
        if(state==1.0f) {
        	BGRenderer.material.SetColor("_Color", Color.Lerp(BGRenderer.material.color,new Color(0,0,0,0),0.1f));
        	transform.position=Vector3.Lerp(transform.position, new Vector3(-2.5f,-1.2f,7.4f),0.1f);
        	transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(72.50101f,242.63f,63.723f), 0.1f);
        	transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(-1f,1f,1f),0.1f);
        	Tamagotchi.position = Vector3.Lerp(Tamagotchi.position, new Vector3(0f,-3f,10.87f),0.1f);
        	Tamagotchi.rotation = Quaternion.Slerp(Tamagotchi.rotation, Quaternion.Euler(116.628f,435.167f,68.529f), 0.1f);
        	Tamagotchi.localScale = Vector3.Lerp(Tamagotchi.localScale, new Vector3(0f,0f,0f),0.1f);
        	CrowdSounds.volume=Mathf.Lerp(CrowdSounds.volume, 0.5f,0.1f);
        }
        if(state==2.0f) {
        	BGRenderer.material.SetColor("_Color", Color.Lerp(BGRenderer.material.color,new Color(0,0,0,0),0.1f));
        	transform.position=Vector3.Lerp(transform.position, new Vector3(0.5f,-2f,-3.186f),0.1f);
        	transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(116.628f,435.167f,68.529f), 0.1f);
        	transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(-1f,1f,1f),0.1f);
        	Tamagotchi.position = Vector3.Lerp(Tamagotchi.position, new Vector3(0f,-3f,10.87f),0.1f);
        	Tamagotchi.rotation = Quaternion.Slerp(Tamagotchi.rotation, Quaternion.Euler(116.628f,435.167f,68.529f), 0.1f);
        	Tamagotchi.localScale = Vector3.Lerp(Tamagotchi.localScale, new Vector3(0f,0f,0f),0.1f);
        	ShugorLogo.localScale=new Vector3(0f,0f,0f);
        	FadeBackground.position=FBGPos;
        	timer=0f;
        	hasPlayed=false;
        	CrowdSounds.volume=Mathf.Lerp(CrowdSounds.volume, 0.5f,0.1f);
        }
        if(state>=3.0f) {
        	BGRenderer.material.SetColor("_Color", Color.Lerp(BGRenderer.material.color,DarkColor,0.1f));
        	transform.position=Vector3.Lerp(transform.position, new Vector3(0.0f,0f,0f),0.1f);
        	transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(180f,90f,0f), 0.1f);
        	transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0f,0f,0f),0.1f);
        	Tamagotchi.position = Vector3.Lerp(Tamagotchi.position, new Vector3(0f,0.3f,-10.87f),0.1f);
        	Tamagotchi.rotation = Quaternion.Slerp(Tamagotchi.rotation, Quaternion.Euler(0f,-180f,0f), 0.1f);
        	Tamagotchi.localScale = Vector3.Lerp(Tamagotchi.localScale, new Vector3(1f,1f,1f),0.1f);
        	timer+=1*Time.deltaTime;
        	CrowdSounds.volume=Mathf.Lerp(CrowdSounds.volume, 0f,0.1f);

        	if (timer>=3f && timer<8f) {
        		if (hasPlayed==false) {
        			audioSource.PlayOneShot(ShugorJingle,0.7f);
        			hasPlayed=true;
        		}
        		ShugorLogo.localScale=Vector3.Lerp(ShugorLogo.localScale, new Vector3(1f,1f,1f),0.4f);
        	}
        	if (timer>8f) {
        		ShugorLogo.localScale=Vector3.Lerp(ShugorLogo.localScale, new Vector3(0f,0f,0f),0.4f);
        	}
        	if (timer>=10f) {
        		FadeBackground.position=Vector3.Lerp(FadeBackground.position,new Vector3(0f,0f,-14f),0.1f);
        	}
        	if (timer>=13f) {
        		SceneManager.LoadScene (sceneName:nextScene);
        	}
        }
    }
}
