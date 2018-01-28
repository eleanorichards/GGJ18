using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour {

	[SerializeField] Material lit_mat;
	[SerializeField] Material unlit_mat;

	[SerializeField] Renderer ren;
	private bool lit = false;

	[SerializeField] int counter = 0;

	// Use this for initialization
	void Start () {
		ren = GetComponent<Renderer> ();

		ren.material = unlit_mat;
		
	}
	
	// Update is called once per frame
	void Update () {

		counter += 1;

		if (counter > 200) 
		{
			counter = 0;

			if (lit)
			{
				ren.material = unlit_mat;
				lit = false;
			} 

			else 
			{
				ren.material = lit_mat;
				lit = true;
			}
		}
		
	}
}
