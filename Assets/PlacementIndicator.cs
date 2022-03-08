using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{
    private ARRaycastManager _raycastManager;

    private enum ObjectTags
    {
        Cube
    }
    
    // Placement indicator
    private GameObject _visual;

    private void Start()
    {
        // Get the components
        _raycastManager = FindObjectOfType<ARRaycastManager>();
        // Because it is setup in the editor as child
        _visual = transform.GetChild(0).gameObject;
        
        // Hide the indicator at first
        _visual.SetActive(false);
    }

    private void Update()
    {
        

        DisplaySpawnerCircle();

    }

    private void DisplaySpawnerCircle()
    {
        // Shoot a raycast from the center of the screen
        List<ARRaycastHit> hits = new List<ARRaycastHit>();


        // First argument is the screen position, second is the list of hits 
        _raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        // If we hit a plane, update the position of the indicator
        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            if (!_visual.activeInHierarchy)
                _visual.SetActive(true);
        }
    }
}
