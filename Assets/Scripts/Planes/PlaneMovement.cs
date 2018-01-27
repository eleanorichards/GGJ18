using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    private Aeroplane plane = null;
    private BezierCurve trajectory = null;
    private int indexNum = 0;
    private float progress = 0.0f;

	// Use this for initialization


	public void InitialisePlane()
	{
		plane = gameObject.GetComponent<Aeroplane>();
		indexNum = plane.indexNum;

		trajectory = getTrajectory (); 
	}

	BezierCurve getTrajectory()
	{
		BezierCurve[] trajectories = GameObject.FindObjectsOfType<BezierCurve>();
		foreach(BezierCurve _trajectory in trajectories)
		{
			if (_trajectory.indexNum == plane.getIndexNum())
			{
				return _trajectory;
			}
		}
		return null; 
	}
	// Update is called once per frame
	void FixedUpdate ()
    {
		if (trajectory.IsInitialised) 
		{

			progress += 0.01f;
			
			if (progress < 1.0f) {
				Vector3 NewPos = trajectory.GetPoint (progress);
				transform.localPosition = NewPos;// new Vector3(NewPos.x, NewPos.z, NewPos.y);
				transform.LookAt (NewPos + trajectory.GetDirection (progress));
			} else
				progress = 0; 
		}
        
    }
}
