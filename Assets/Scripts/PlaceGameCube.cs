using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceGameCube : MonoBehaviour
{
    // Public variables can be set from the unity UI.

    // These will store references to our other components.
    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;


    // This will indicate whether the Game cube is set.
    private bool placed = false;
    public int cubeCount = 0;
    // Create variables for adding cubes on ar area
    public GameObject cubeObject;
    // Store these cube objects for furhte use
    private List<GameObject> cubeObjects = new List<GameObject>();
    private Pose hitPose;
    // Start is called before the first frame update.
    public Text messageText;


    //Delete later if not neccesary
    private Camera arCamera;
    void Start()
    {
        // GetComponent allows us to reference other parts of this game object.
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
        arCamera = GetComponent<ARSessionOrigin>().camera;
    }

    // Update is called once per frame.
    void Update()
    {
        if (!placed)
        {
            if (Input.touchCount > 0)
            {
                Vector2 touchPosition = Input.GetTouch(0).position;

                // Raycast will return a list of all planes intersected by the
                // ray as well as the intersection point.
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if (raycastManager.Raycast(
                    touchPosition, hits, TrackableType.PlaneWithinPolygon))
                {
                    // The list is sorted by distance so to get the location
            
                    hitPose = hits[0].pose;
                    //Activating cube at chosen location
                    InstantiateCube();

                    // After we have placed the game board we will disable the
                    // planes in the scene as we no longer need them.
                    planeManager.SetTrackablesActive(false);

                }
            }
        }
        else
        {
            // The plane manager will set newly detected planes to active by 
            // default so we will continue to disable these.
            planeManager.SetTrackablesActive(false);
        }

    }

    private void InstantiateCube()
    {
        //Instantiate a cube 
        GameObject clone = Instantiate(cubeObject, hitPose.position, hitPose.rotation);
        clone.SetActive(true);

        // Add cube count
        cubeCount = cubeCount + 1;
        placed = true;
        // Add the cube oject to list
        addObject(clone);
        planeManager.SetTrackablesActive(false);
       
      
       
    }

    private void addObject(GameObject obj)
    {
        // Function to add cube object into a list
        cubeObjects.Add(obj);
    }

    // If the user places the game board at an undesirable location we 
    // would like to allow the user to move the game board to a new location.
    public void AllowMoveGameBoard()
    {
        // FUnction to add another cube
        placed = false;
        planeManager.SetTrackablesActive(true);
    }


    public bool Placed()
    {
        // function to check if the cube is placed
        return placed;
    }

    public int GetCubeCount()
    {
        // function to get cube count
        return cubeCount;
    }

    public List<GameObject> getGameObjects()
    {
        // function to get cube objects
        return cubeObjects;
    }

}

