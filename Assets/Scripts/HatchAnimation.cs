using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchAnimation : MonoBehaviour
{
	public Transform MainCamera;
	public Transform TopHalf;
	public Transform BottomHalf;
	public Transform DirLight;
	public Light Lighter;
    // Start is called before the first frame update
    void Start()
    {
        Lighter=DirLight.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
