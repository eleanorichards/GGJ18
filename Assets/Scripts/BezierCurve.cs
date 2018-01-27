using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
	public Vector3[] nodes;
    public GameObject segments;

    public int indexNum;

    private Vector2 startPos = Vector2.zero;
    private Vector3 initialStep = Vector3.zero;

	public bool IsInitialised = false;

    void Start ()
    {       
		
    }

	public void InitialiseNodeArray()
	{
		//AddCurve();

		Debug.Log (nodes.Length);
		IsInitialised = true;
	}

	public void SetIndexNum(int _indexNum)
	{
		indexNum = _indexNum;
	}
	
    public void SetRoute(Vector2 _node0Pos, Vector2 _node1Pos, Vector2 _node2Pos, Vector2 _nodeNPos)
    {     
		
		nodes[0] = new Vector3(_node0Pos.x, 0, _node0Pos.y); 
		nodes[1] = new Vector3(_node1Pos.x, 0, _node1Pos.y);
		nodes[2] = new Vector3(_node2Pos.x, 0, _node2Pos.y);
		nodes[3] = new Vector3(_nodeNPos.x, 0, _nodeNPos.y);
        //initialStep = (_node1Pos - _node0Pos);
        //Debug.Log(initialStep);
        //for (int i = 1; i < nodes.Length; i++)
        //{
        //    nodes[i] = (nodes[i - 1] + initialStep);           
        //}        
    }

	public void SetLandingRoute(GameObject _landingStrip, GameObject _plane)
	{
		nodes [0] =  new Vector3(_plane.transform.position);
		nodes [1] =  new Vector3(_landingStrip.GetComponentInChildren<Transform> ("landingNode1").position);
		nodes [2] =  new Vector3(_landingStrip.GetComponentInChildren<Transform> ("landingNode2").position);
		nodes [3] =  new Vector3(nodes [2]);
	}

    public Vector3 GetPoint(float t)
    {
       //
		int i;
        if (t >= 1f)
            {
                t = 1f;
                i = nodes.Length - 4;
            }
            else 
            {
                t = Mathf.Clamp01(t) * CurveCount;
                i = (int)t;
                t -= i;
                i *= 3;
            }
		//return transform.TransformPoint(Bezier.GetPoint(nodes[i], nodes[i + 1], nodes[i + 2], nodes[i + 3], t));
		return Bezier.GetPoint(nodes[i], nodes[i + 1], nodes[i + 2], nodes[i + 3], t);
    }

	/// <summary>
	/// Adds 3 nodes to the current curve
	/// </summary>
    public void AddCurve()
    {
        Vector3 node = nodes[nodes.Length - 1];
        System.Array.Resize(ref nodes, nodes.Length + 3);
        node.y += 1f;
        nodes[nodes.Length - 3] = node;
        node.y += 1f;
        nodes[nodes.Length - 2] = node;
        node.y += 1f;
        nodes[nodes.Length - 1] = node;
    }


    public int CurveCount
    {
        get
        {
            return (nodes.Length - 1) / 3;
        }
    }

    public Vector3 GetDirection(float t)
    {
        return GetVelocity(t).normalized;
    }

    //returns magnitude of direction
    public Vector3 GetVelocity(float t)
    {
        int i;
        if (t >= 1f)
        {
            t = 1f;
            i = nodes.Length - 4;
        }
        else
        {
            t = Mathf.Clamp01(t) * CurveCount;
            i = (int)t;
            t -= i;
            i *= 3;
        }
		return transform.TransformPoint(Bezier.GetFirstDerivative(
            nodes[i], nodes[i + 1], nodes[i + 2], nodes[i + 3], t)) - transform.position;
    }

    public Vector3 GetNormal3D(float t, Vector3 up)
    {
        Vector3 tng = GetDirection(t);
        Vector3 binormal = Vector3.Cross(up, tng).normalized;
        return Vector3.Cross(tng, binormal);
    }

    public Quaternion GetOrientation3D(float t, Vector3 up)
    {
        Vector3 tng = GetDirection(t);
        Vector3 nrm = GetNormal3D(t, up);
        return Quaternion.LookRotation(tng, nrm);
    }

}
