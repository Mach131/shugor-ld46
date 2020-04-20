using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{

	private Renderer render;
	private Color DarkColor;
	private float LerpAlpha=1f;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        DarkColor=render.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        LerpAlpha=Mathf.Lerp(LerpAlpha,-1,0.05f);
        render.material.SetColor("_Color", new Color(DarkColor.r,DarkColor.g,DarkColor.b,LerpAlpha));
        if (LerpAlpha<-0.75f)
        {
        	gameObject.SetActive(false);
        }
    }
}
