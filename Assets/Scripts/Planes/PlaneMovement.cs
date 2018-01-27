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
	void Start ()
    {
        plane = gameObject.GetComponent<Aeroplane>();
        indexNum = plane.indexNum;
        trajectory = plane.trajectory;
	}
	
	// Update is called once per frame
	void Update ()
    {
        progress += Time.deltaTime;

        if(progress < 1)
        {
            Vector3 position = trajectory.GetPoint(progress);
            transform.localPosition = position;        
            transform.LookAt(position + trajectory.GetDirection(progress));
        }
        
    }
}
