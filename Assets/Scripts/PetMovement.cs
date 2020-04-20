using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMovement : MonoBehaviour
{
	
  [SerializeField] private Vector3 pointA = new Vector3(5, 1, 2);
  [SerializeField] private Vector3 pointB = new Vector3(5, 2, 2);
  [SerializeField] private float speed = 1;
  
  private float t; //t stands for time
  
  private void Update()
  {
	  t+= Time.deltaTime * speed;
	  
	  //moves pet to point B
	  transform.position = Vector3.Lerp(pointA, pointB, t);
	  
	  //Swaps point A and B, one pet reaches point B
	  if (t >= 1)
	  {
		  
		  var b = pointB;
		  var a = pointA;
		  
		  pointA = b;
		  pointB = a;
		  
		  t = 0;
	  }
	  
  }
  
	//The LERP
	private Vector3 CustomLerp(Vector3 a, Vector3 b, float t)
	{
		return a * (1 - t) + b * t;
		
	}
	
}