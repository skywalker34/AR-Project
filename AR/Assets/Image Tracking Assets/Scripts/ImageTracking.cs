using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ImageTracking : MonoBehaviour
{
    [SerializeField] ARTrackedImageManager aRTrackedImageManager;
    [SerializeField] GameObject spherePrefab;

    GameObject instantiateObject;
    void OnEnable()
    {
        aRTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    void OnDisable()
    {
        aRTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    void OnImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {

        foreach(var trackedImage in eventArgs.added)
        {
            if(trackedImage.referenceImage.name == "Logo_NHL")
            {
                instantiateObject = 
                    Instantiate(spherePrefab, trackedImage.transform.position, trackedImage.transform.rotation);
            }
        }
        foreach (var trackedImage in eventArgs.added)
        {
            if (trackedImage.referenceImage.name == "Logo_NHL")
            {
                instantiateObject.transform.position = trackedImage.transform.position;
            }
        }

        foreach (var trackedImage in eventArgs.added)
        {
            if (trackedImage.referenceImage.name == "Logo_NHL")
            {
                Destroy(instantiateObject);
            }
        }




    }
}