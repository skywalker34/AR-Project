using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ImageTracking : MonoBehaviour
{
    [SerializeField] ARTrackedImageManager arTrackedImageManager;
    [SerializeField] GameObject objectPrefab;
    GameObject instantiatedObject;

    void OnEnable()
    {
        arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    void OnDisable()
    {
        arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    void OnImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(var trackedImage in eventArgs.added)
        {
            if(trackedImage.referenceImage.name == "Logo_NHL")
            {
                instantiatedObject = Instantiate(objectPrefab, trackedImage.transform.position,
                    trackedImage.transform.rotation);
            }
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            if (trackedImage.referenceImage.name == "Logo_NHL")
            {
                instantiatedObject.transform.position = trackedImage.transform.position;
            }
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            if (trackedImage.referenceImage.name == "Logo_NHL")
            {
                Destroy(instantiatedObject);
            }
        }
    }
}