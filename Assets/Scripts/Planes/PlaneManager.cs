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

    private Vector2 node0Pos = Vector2.zero;
    private Vector2 node1Pos = Vector2.zero;
    private Vector2 nodeNPos = Vector2.zero;

    // Use this for initialization
    void Start ()
    {
        topBoundary = GameObject.Find("topWall").transform.position.x;
        bottomBoundary = GameObject.Find("bottomWall").transform.position.x;
        leftBoundary = GameObject.Find("leftWall").transform.position.z;
        rightBoundary = GameObject.Find("rightWall").transform.position.z;
		SpawnPlane();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        currentTime += Time.deltaTime;
        if(currentTime > spawnInterval)
        {
            //SpawnPlane();
            currentTime = 0;
            Debug.Log("Plane spawned");
        }
    }
    
    void SpawnPlane()
    {
		BezierCurve trajectoryComponent = Instantiate(trajectoryPrefab);
		trajectory.Add(trajectoryComponent);
		trajectoryComponent.SetIndexNum (indexNum);
		SetTrajectory(trajectoryComponent);
		trajectoryComponent.SetRoute(node0Pos, node1Pos, nodeNPos);     
        trajectory.Sort();

		Aeroplane planeComponent = Instantiate(planePrefab) as Aeroplane;
		planes.Add(planeComponent);
		planeComponent.SetIndexNum (indexNum);
        planes.Sort();
        SendToConsole(planes[indexNum]);

        indexNum++;
    }

    void SetTrajectory(BezierCurve _trajectory)
    {
        int wallSelect = 0;
        

        float y = 0.0f;
        //set starter wall
        wallSelect = Random.Range(0, 4);
        switch (wallSelect)
        {
            case 0: //TOP
                y = Random.Range(leftBoundary, rightBoundary);
                node0Pos = new Vector2(topBoundary, y);
                node1Pos = new Vector2(topBoundary - Random.Range(80, 120), y + Random.Range(-20, 20));
                break;
            case 1: //BOTTOM
                y = Random.Range(leftBoundary, rightBoundary);
                node0Pos = new Vector2(bottomBoundary, y);
                node1Pos = new Vector2(topBoundary - Random.Range(80, 120), y + Random.Range(-20, 20));
                break;
            case 2: //LEFT
                y = Random.Range(bottomBoundary, topBoundary);
                node0Pos = new Vector2(y, leftBoundary);
                node1Pos = new Vector2(y + Random.Range(-20, 20), leftBoundary + Random.Range(80, 120));
                //if(nodeNPos)
                break;
            case 3: //RIGHT
                y = Random.Range(bottomBoundary, topBoundary);
                node0Pos = new Vector2(y, rightBoundary);
                node1Pos = new Vector2(y + Random.Range(-20, 20), rightBoundary - Random.Range(80, 120));
                break;
            default:
                node0Pos = new Vector2(topBoundary, leftBoundary);
                break;
        }
    }

    void SetNthNode()
    {
        Vector2 initialStep = Vector2.zero;
        nodeNPos = node1Pos;
        initialStep = (node1Pos - node0Pos);
        Debug.Log(initialStep);
        if(nodeNPos.y > leftBoundary && nodeNPos.y < rightBoundary && nodeNPos.x < bottomBoundary && nodeNPos.x > topBoundary)
        {
            nodeNPos += initialStep;
        }
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
