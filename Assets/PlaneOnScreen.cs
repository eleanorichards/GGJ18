using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneOnScreen : MonoBehaviour 
{
	// Use this for initialization
	Color givenColour; 
	int fuelLevel; 
	bool bingoFuel = false; 
	GameObject plane; 
	public bool button; 
	void Start () 
	{
		givenColour = Color.green; 
		fuelLevel = 10; 
		//colour get 	
		//getcomponent plane
		//getcomponent button with colour givencolour
		InvokeRepeating("DrainFuel", 0, 1); 
	}
	void DrainFuel()
	{
		fuelLevel--; 
	}
	// Update is called once per frame
	void Update () 
	{
		//transform.localPosition = new Vector3 (plane.transform.position.z / scale, transform.localPosition.y, plane.transform.position.x / scale);
		//plane.x / 320 plane.y / 200     x = -0.4 + (0.8 * ans1)  y = -0.42 + (0.5 * ans2)
		//float ansx = 320;
		//float ansy = 200; 
		//transform.localPosition = new Vector3 (ansy, transform.localPosition.y, ansx);
		if (fuelLevel < 0) 
		{
			//Crash
		}
		if (fuelLevel < 25 && bingoFuel == false) 
		{
			InvokeRepeating("Flash", 0, 0.5f);
			bingoFuel = true; 
		}
	}
	void Flash()
	{
		if (GetComponent<SpriteRenderer>().color == Color.red)
			GetComponent<SpriteRenderer>().color = givenColour;
		else
			GetComponent<SpriteRenderer> ().color = Color.red;
	}

}
