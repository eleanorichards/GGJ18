using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGrounding : MonoBehaviour 
{
	private BezierCurve trajectory;
	private Aeroplane plane;

	public void InitialiseGroundingSystem()
	{
		plane = gameObject.GetComponent<Aeroplane>();
		trajectory = plane.GetComponent<PlaneMovement>().getTrajectory();
	}


	void LandPlane(GameObject _runway)
	{
		Vector3 landingNode = _runway.GetComponentInChildren<Transform> ().position;
	}

	void CrashPlane()
	{

	}

	void TakeOffPlane()
	{

	}
}
