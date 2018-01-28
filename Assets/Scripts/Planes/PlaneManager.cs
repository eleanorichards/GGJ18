using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    public Aeroplane planePrefab;
    public BezierCurve trajectoryPrefab;

    public List<Aeroplane> planes = new List<Aeroplane>(10);
    public List<BezierCurve> trajectory = new List<BezierCurve>(10);
	public GameObject[]	runways;
	private RadarScreen radarScreen;
    public int spawnInterval = 10;

    private float topBoundary = 0.0f;
    private float bottomBoundary = 0.0f;
    private float leftBoundary = 0.0f;
    private float rightBoundary = 0.0f;

    private float currentTime = 0.0f;

    private int indexNum = 0;

    // Use this for initialization
    void Start ()
    {
		runways = GameObject.FindGameObjectsWithTag ("Runway");
        topBoundary = GameObject.Find("topWall").transform.position.x /2;
        bottomBoundary = GameObject.Find("bottomWall").transform.position.x /2;
        leftBoundary = GameObject.Find("leftWall").transform.position.z /2;
        rightBoundary = GameObject.Find("rightWall").transform.position.z /2;
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
		planeComponent.SetIndexNum (indexNum);
		planeComponent.GetComponent<PlaneMovement> ().InitialisePlane (getTrajectory(indexNum));
		planes.Add(planeComponent);
		SendToConsole(planes[indexNum]);
		//radarScreen.SpawnPlaneOnScreen (/*planeComponent*/);
		indexNum++;
		
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
    
	public void LandLeverPulled()
	{
		//int planeNum = InputManager.GetButtonDown("planeSelection");
		//int runwayNum = InputManager.GetButtonDown("runwaySelection");
		/*if (runways[runwayNum].GetComponent<Runway>().IsPlaneInTrigger(planes[planeNum]) 
			&& runways[runwayNum].GetComponent<Runway>().IsPlaneAligned(planes[planeNum].transform.rotation.y))
		{
			LandPlane(planeNum, runwayNum);
		}*/

	}

	public void TakeOffLeverPulled(int _planeNum)
	{
		/*int planeNum = InputManager.GetButtonDown("planeSelection");
		int runwayNum = InputManager.GetButtonDown("planeSelection");
		planes[planeNum].transform.eulerAngles.y += 180; 
		TakeOffPlane(planeNum, runwayNum); */
	}


    void SetTrajectory(BezierCurve _trajectory)
    {
		
        int wallSelect = 0;
        float y = 0.0f;

		Vector3 node0Pos = Vector3.zero;
		Vector3 node1Pos = Vector3.zero;
		Vector3 node2Pos = Vector3.zero;
		Vector3 nodeNPos = Vector3.zero;
		Vector3 initialStep = Vector3.zero;

        //set starter wall
        wallSelect = Random.Range(0, 4);
		float RAND = Random.Range (-20, 20);
		float LARGERAND	= Random.Range (80, 120);

		wallSelect = 0;

        switch (wallSelect)
        {
			case 0: //TOP
				y = Random.Range (leftBoundary+20, rightBoundary-20);
				node0Pos = new Vector3 (topBoundary+20, 0, y);
			node1Pos = node0Pos + new Vector3 (100, 0, 0);
			node2Pos = node1Pos + new Vector3(100, 0, 0);
	            break;
	        case 1: //BOTTOM
	            y = Random.Range(leftBoundary+20, rightBoundary-20);
			node0Pos = new Vector3(bottomBoundary-20, 0, y);
			node1Pos = node0Pos + new Vector3(-100, 0, 0);
			node2Pos = node1Pos + new Vector3(-100,0,  0);

	            break;
	        case 2: //LEFT
	            y = Random.Range(topBoundary+20, bottomBoundary-20);
	            node0Pos = new Vector3(y, leftBoundary+20);
			node1Pos = node0Pos + new Vector3(0 , 0, 100);
			node2Pos = node1Pos + new Vector3(0 , 0, 100);
	            break;
	        case 3: //RIGHT
				y = Random.Range(topBoundary+20, bottomBoundary-20);
			node0Pos = new Vector3(y, 0, rightBoundary-20);
			node1Pos = node0Pos + new Vector3(0 , 0, -100);
			node2Pos = node1Pos + new Vector3(0, 0, -100);

	            break;
	        default:
			node0Pos = new Vector3(topBoundary, 0, leftBoundary);
			node1Pos = node0Pos + new Vector3(y , 0, rightBoundary - 100);
			node2Pos = node1Pos + new Vector3(y , 0, leftBoundary + 100);
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

	//finished - called if all criteria are met
	public void LandPlane(int runwayNum, int planeNum)
	{
		Vector3 node0Pos = Vector3.zero;
		Vector3 node1Pos = Vector3.zero;
		Vector3 node2Pos = Vector3.zero;
		Vector3 nodeNPos = Vector3.zero;
		Vector3 initialStep = Vector3.zero;

		//I'm sorry about the GetComponent thing, but get the two points
		//attached to the runway prefabs, //transform1 = node3, //transform2 = node4
		//y value gradually decreases
		/*node0Pos =  planes[planeNum].transform.position;
		node1Pos =  new Vector3(planes[planeNum].transform.position.x, planes[planeNum].transform.position.y/2, 
			runways[runwayNum].GetComponentInChildren<Transform> ("Transform1").position.z);
		node2Pos =  new Vector3(runways[runwayNum].GetComponentInChildren<Transform> ("Transform1").position.x, 0, 
			runways[runwayNum].GetComponentInChildren<Transform> ("Transform1").position.z);
		nodeNPos =  runways[runwayNum].GetComponentInChildren<Transform> ("Transform2").position;*/

		getTrajectory(planeNum).SetRoute(node0Pos, node1Pos, node2Pos, nodeNPos);
	}

	public void CrashPlane(int planeNum)
	{
		//DestroyPlane
		//planes[planeNum].Destroy
	}

	//finished
	//set trajectory from selected on 2D console
	public void SetNewTrajectory(int planeNum, Vector3 newPos)
	{
		Vector3 node0Pos = Vector3.zero;
		Vector3 node1Pos = Vector3.zero;
		Vector3 node2Pos = Vector3.zero;
		Vector3 nodeNPos = Vector3.zero;
		Vector3 initialStep = Vector3.zero;

		node0Pos =  planes[planeNum].transform.position;
		node1Pos =  new Vector3(planes[planeNum].transform.position.x,  newPos.y);
		node2Pos =  newPos;
		//initialStep == last step taken (repeate the line until you hit the border)
		initialStep = node2Pos - node1Pos;
		while (nodeNPos.y > leftBoundary*3f && nodeNPos.y < rightBoundary*3f && nodeNPos.x < bottomBoundary*3f && nodeNPos.x > topBoundary*3f) 
		{
			nodeNPos += initialStep;
		}

		getTrajectory(planeNum).SetRoute(node0Pos, node1Pos, node2Pos, nodeNPos);
	}


    public void SendToConsole(Aeroplane _plane)
    {
        //variable in console: public Aeroplane
    }

    void DestroyPlane()
    {
       // planes.FindIndex(2);
	}




	void TakeOffPlane(int planeNum, int runwayNum)
	{
		Vector3 node0Pos = Vector3.zero;
		Vector3 node1Pos = Vector3.zero;
		Vector3 node2Pos = Vector3.zero;
		Vector3 nodeNPos = Vector3.zero;
		Vector3 initialStep = Vector3.zero;

		//transform2
		//transform1 (.y + 25)
		// repeat until off border
		//node0Pos =  runways[runwayNum].GetComponentInChildren<Transform> ("Transform2").position;
		//node1Pos =  runways[runwayNum].GetComponentInChildren<Transform> ("Transform1").position;
		initialStep = node1Pos - node0Pos;
		node2Pos =  initialStep + (node1Pos - node0Pos);

		while (nodeNPos.y > leftBoundary*3f && nodeNPos.y < rightBoundary*3f && nodeNPos.x < bottomBoundary*3f && nodeNPos.x > topBoundary*3f) 
		{
			nodeNPos += initialStep;
		}

		getTrajectory(planeNum).SetRoute(node0Pos, node1Pos, node2Pos, nodeNPos);
	}

	public BezierCurve getTrajectory(int planeNum)
	{
		BezierCurve[] trajectories = GameObject.FindObjectsOfType<BezierCurve>();
		foreach(BezierCurve _trajectory in trajectories)
		{
			if (_trajectory.indexNum == planes[planeNum].getIndexNum())
			{
				return _trajectory;
			}
		}
		return null; 

	}
}
