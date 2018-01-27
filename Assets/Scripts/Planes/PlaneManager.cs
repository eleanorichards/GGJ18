using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    public Aeroplane planePrefab;
    public BezierCurve trajectoryPrefab;

    public List<Aeroplane> planes = new List<Aeroplane>(10);
    public List<BezierCurve> trajectory = new List<BezierCurve>(10);

    public int spawnInterval = 10;

    private float topBoundary = 0.0f;
    private float bottomBoundary = 0.0f;
    private float leftBoundary = 0.0f;
    private float rightBoundary = 0.0f;

    private float currentTime = 0.0f;

    private int indexNum = 0;

//	private Vector2 node0Pos = Vector2.zero;
//	private Vector2 node1Pos = Vector2.zero;
//	private Vector2 node2Pos = Vector2.zero;
//	private Vector2 nodeNPos = Vector2.zero;
//	private Vector2 initialStep = Vector2.zero;

    // Use this for initialization
    void Start ()
    {
		//trajectoryPrefab.nodes = 1; 
        topBoundary = GameObject.Find("topWall").transform.position.x /2;
        bottomBoundary = GameObject.Find("bottomWall").transform.position.x /2;
        leftBoundary = GameObject.Find("leftWall").transform.position.z /2;
        rightBoundary = GameObject.Find("rightWall").transform.position.z /2;
//		SpawnTrajectory ();
//		SpawnPlane();
//		currentTime = 0;
//		Debug.Log("Plane spawned");
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        currentTime += Time.deltaTime;
        if(currentTime > spawnInterval)
        {
			SpawnTrajectory ();
            SpawnPlane();
            currentTime = 0;
            Debug.Log("Plane spawned");
        }
    }
    
	void SpawnTrajectory()
	{
		BezierCurve trajectoryComponent = Instantiate(trajectoryPrefab);
		//trajectoryComponent.InitialiseNodeArray ();
		trajectory.Add(trajectoryComponent);
		trajectoryComponent.SetIndexNum (indexNum);
		SetTrajectory(trajectoryComponent);	
	}


    void SpawnPlane()
    {
		Aeroplane planeComponent = Instantiate(planePrefab) as Aeroplane;
		planeComponent.GetComponent<PlaneMovement> ().InitialisePlane ();
		planes.Add(planeComponent);
		planeComponent.SetIndexNum (indexNum);
        SendToConsole(planes[indexNum]);
        indexNum++;
    }

    void SetTrajectory(BezierCurve _trajectory)
    {
		
        int wallSelect = 0;
        float y = 0.0f;
		Vector2 node0Pos = Vector2.zero;
		Vector2 node1Pos = Vector2.zero;
		Vector2 node2Pos = Vector2.zero;
		Vector2 nodeNPos = Vector2.zero;
		Vector2 initialStep = Vector2.zero;

        //set starter wall
        wallSelect = Random.Range(0, 4);
		float RAND = Random.Range (-20, 20);
		float LARGERAND	= Random.Range (80, 120);
		//wallSelect = 0;
        switch (wallSelect)
        {
			case 0: //TOP
				y = Random.Range (leftBoundary+20, rightBoundary-20);
				node0Pos = new Vector2 (topBoundary+20, y);
				node1Pos = node0Pos + new Vector2 (100, 0);
				node2Pos = node1Pos + new Vector2(100, 0);
	            break;
	        case 1: //BOTTOM
	            y = Random.Range(leftBoundary+20, rightBoundary-20);
	            node0Pos = new Vector2(bottomBoundary-20, y);
				node1Pos = node0Pos + new Vector2(-100, 0);
				node2Pos = node1Pos + new Vector2(-100, 0);

	            break;
	        case 2: //LEFT
	            y = Random.Range(topBoundary+20, bottomBoundary-20);
	            node0Pos = new Vector2(y, leftBoundary+20);
				node1Pos = node0Pos + new Vector2(0 , 100);
				node2Pos = node1Pos + new Vector2(0 , 100);
	            break;
	        case 3: //RIGHT
				y = Random.Range(topBoundary+20, bottomBoundary-20);
	            node0Pos = new Vector2(y, rightBoundary-20);
				node1Pos = node0Pos + new Vector2(0 , -100);
				node2Pos = node1Pos + new Vector2(0, -100);

	            break;
	        default:
	            node0Pos = new Vector2(topBoundary, leftBoundary);
			node1Pos = node0Pos + new Vector2(y , rightBoundary - 100);
				node2Pos = node1Pos + new Vector2(y , leftBoundary + 100);
	            break;
        }
		initialStep = (node1Pos - node0Pos);
		nodeNPos = node2Pos;
		while (nodeNPos.y > leftBoundary*3f && nodeNPos.y < rightBoundary*3f && nodeNPos.x < bottomBoundary*3f && nodeNPos.x > topBoundary*3f) 
		{
			nodeNPos += initialStep;
		}
		_trajectory.SetRoute(node0Pos, node1Pos, node2Pos, nodeNPos);     
    }

    void SendToConsole(Aeroplane _plane)
    {
        //variable in console: public Aeroplane
    }

    void DestroyPlane()
    {
       // planes.FindIndex(2);
    }
}
