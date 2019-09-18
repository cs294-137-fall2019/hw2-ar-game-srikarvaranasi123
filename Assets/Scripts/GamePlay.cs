using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class GamePlay : MonoBehaviour
{
    // Set parameters if playing game
    private bool playGame = false;
    private GameObject x;

    //Get the Camera
    private Camera arCamera;

    // Initialize the required objects
    private PlaceGameCube placeGameCube;
    private ARRaycastManager arRaycastManager;
    private Vector2 touchPosition = default;
    private List<GameObject> objects;

    // Text objects
    public Text messageText;
    public Text winText;

    void Start()
    {
        // Get the current AR camera
        arCamera = GetComponent<ARSessionOrigin>().camera;
   
        //Get Placegamecube components
        placeGameCube = GetComponent<PlaceGameCube>();
       
        arRaycastManager = GetComponent<ARRaycastManager>();


    }

    // Update is called once per frame
    void Update()
    {
      // Check if wea re in player profile
        if (playGame)
        {
            // Get the cube objects
            objects = placeGameCube.getGameObjects();
            // Display the number of cubes
            messageText.gameObject.SetActive(true);
            messageText.text = "Cubes: " + objects.Count;

            // Check if the camera is able to see the cubes
            foreach (var obj in objects)
            {
                Vector3 viewPos = arCamera.WorldToViewportPoint(obj.transform.position);
                // Whenever Camera finds the object on the screen
                if (viewPos.x > 0 && viewPos.x < 1 && viewPos.y>0 && viewPos.y < 1 && viewPos.z > 0)
                {
                    // Wait for sometime
                    WaitRemoval();
                    // Remove the cube from screen
                    obj.SetActive(false);
                    objects.Remove(obj);
                }

            }
            // Win condition
            if (objects.Count == 0)
            {
                winText.gameObject.SetActive(true);
                winText.text = "WIN ";
                playGame = false;
            }

        }
    }

    private IEnumerator WaitRemoval()
    {
        // Function to wait for sometime
        yield return new WaitForSeconds(1);
    }

    public void setPlayGame()
    {
        playGame = true;
    }
}
