using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{

    public GameObject Trail;
    public float minCuttingVelocity = 0.00001f; // variable to ensure that player is slicing and not clicking "produce"
    bool isCutting = false;

    Vector2 previousPos; // update gameobject pos

    // references
    Rigidbody2D rb;
    Camera cam;
    GameObject currentTrail;

    CircleCollider2D circleCollider;



    private void Start()
    {
        //setting references
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        //main game loop for blade
        if (Input.GetMouseButtonDown(0))
        {
            StartCutting();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }
        if (isCutting)
        {
            UpdateCut();
        }
    }

    void UpdateCut()
    {
        // while is cutting is true, create slice
        Vector2 newPos = cam.ScreenToWorldPoint(Input.mousePosition);
        rb.position = newPos;

        float velocity = (newPos - previousPos).magnitude * Time.deltaTime;

        if(velocity > minCuttingVelocity)
        {
            circleCollider.enabled = true;
        }
        else
        {
            circleCollider.enabled = false;
        }
        previousPos = newPos;
    }

    private void StartCutting()
    {
        //enable UpdateCut() and instaniate trail WITH FALSE RIGIDBODY to disable "click slicing"
        isCutting = true;
        currentTrail = Instantiate(Trail, transform);
        previousPos = cam.ScreenToWorldPoint(Input.mousePosition);
        circleCollider.enabled = false;
    }

    private void StopCutting()
    {
        //disable UpdateCut() and remove trail instantiation
        isCutting = false;
        currentTrail.transform.SetParent(null);
        Destroy(currentTrail, 2f);
        circleCollider.enabled = false;
    }

}
