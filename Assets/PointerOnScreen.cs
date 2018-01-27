using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerOnScreen : MonoBehaviour 
{

	// Use this for initialization
	[Range(-0.4f,0.4f)] 
	public float xPos; 
	[Range(-0.42f,0.08f)] 
	public float yPos; 
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, xPos); 
		transform.localPosition = new Vector3(yPos, transform.localPosition.y, transform.localPosition.z);  
	}
}
