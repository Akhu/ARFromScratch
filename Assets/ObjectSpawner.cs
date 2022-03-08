using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Color;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    private PlacementIndicator _placementIndicator;
    private bool _mode;
    private float _trust = 20f;

    private void Start()
    {
        _placementIndicator = FindObjectOfType<PlacementIndicator>();
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                Debug.Log("Something Hit");
                if (raycastHit.collider.name == "CubeObject" || raycastHit.collider.CompareTag("Cube"))
                {
                    GameObject cubeTouched = raycastHit.collider.gameObject;
                    cubeTouched.GetComponent<Renderer>().material.color = red;
                    Rigidbody rb = raycastHit.rigidbody;
                    rb.AddForce(transform.forward * _trust, ForceMode.Impulse);
                    
                    Destroy(raycastHit.collider.gameObject, 2.5f);
                }
                else
                {
                    GameObject obj = Instantiate(objectToSpawn, _placementIndicator.transform.position,
                        _placementIndicator.transform.rotation);
                }
            }
        }
    }
}
