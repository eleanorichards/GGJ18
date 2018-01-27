using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runway : MonoBehaviour {
	public bool inTrigger = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider col)
	{
		inTrigger = true;
	}
}
