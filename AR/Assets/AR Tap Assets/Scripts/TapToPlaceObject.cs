using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//Simulation Info
//https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@5.0/manual/xr-simulation/simulation-environments.html

[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlaceObject : MonoBehaviour
{
    // Link Spawnable Object
    public GameObject objectToSpawn;

    // Keep track of SpawnedObject in Scene
    GameObject spawnedObject;

    // Link raycast tracker
    ARRaycastManager aRRaycastManager;

    // Where did the Tap on screen occur
    Vector2 touchPosition;

    // Keep track of all objects being hit by Raycast
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        // Link the Raycast at start
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    // Record the Touch Position
    bool GetTouchPosition(out Vector2 touchPosition)
    {
        // Was there a Touch on Screen?
        if(Input.touchCount > 0)
        {
            // Store the Touch position
            touchPosition = Input.GetTouch(0).position;

            // Return Touch happened
            return true;
        }

        // No Touch
        touchPosition = Vector2.zero;

        // Return Touch did not happened
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isEditor)
        {
            if (Input.GetButton("Fire1"))
            {
                touchPosition = Input.mousePosition;
            }
            else
            {
                // No Touch
                touchPosition = Vector2.zero;

                // Return Touch did not happened
                return;
            }
        }
        else
        {
            // Check to see if there was no Touch event on Screen
            if (!GetTouchPosition(out Vector2 touchPosition))
                return;
        }

        // Touch event occured
        //https://docs.unity3d.com/2019.2/Documentation/ScriptReference/Experimental.XR.TrackableType.html
        if (aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            // Store pose position to use when spawning
            Pose hitPose = hits[0].pose;

            // Check if a spawedObject is in the Scene
            if (spawnedObject == null)
                // Instantiate a new spawnedObject based on screen touch position
                spawnedObject = Instantiate(objectToSpawn, hitPose.position, hitPose.rotation);
            else
                // Update position of spawnedObject if it already exists
                spawnedObject.transform.position = hitPose.position;
        }    
    }
}
