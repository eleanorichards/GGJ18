using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aeroplane : MonoBehaviour
{
    public int indexNum;

    private BezierCurve[] trajectories;

    public BezierCurve trajectory;


	public void SetIndexNum(int _indexNum)
	{
		indexNum = _indexNum;
	}
	
    // Use this for initialization
    void Start ()
    {
        //Find trajectory with associated index num
        trajectories = GameObject.FindObjectsOfType<BezierCurve>();
        foreach(BezierCurve _trajectory in trajectories)
        {
            if (_trajectory.indexNum == indexNum)
            {
                trajectory = _trajectory;
            }
        }
	}
    

    public Vector2 GetXY()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }
}
