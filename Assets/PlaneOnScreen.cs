using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneOnScreen : MonoBehaviour 
{
	// Use this for initialization
	public PlaneManager planeManager; 
	Color givenColour; 
	int fuelLevel; 
	bool bingoFuel = false; 
	Aeroplane plane; 
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
	public void setPlane(Aeroplane pln)
	{
		plane = pln; 
	}
	void DrainFuel()
	{
		fuelLevel--; 
	}
	// Update is called once per frame
	void Update () 
	{
		//transform.localPosition = new Vector3 (plane.transform.position.z / scale, transform.localPosition.y, plane.transform.position.x / scale);
		float ansx = -0.4f + (0.8f * (plane.transform.position.x / 320));
		float ansy = -0.42f + (0.5f *  (plane.transform.position.z / 200)); 
		transform.localPosition = new Vector3 (ansy, transform.localPosition.y, ansx);
		if (fuelLevel < 0) 
		{
			//planeManager.crashPlane (plane); 
			//this.destroy (); 
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
