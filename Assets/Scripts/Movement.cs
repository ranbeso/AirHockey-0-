using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    bool wasJustClicked = true;
    float R;
    int mouseState = 0;

    Rigidbody2D rb;
    public Transform BoundaryHolder;
    Boundary playerBoundary;

    struct Boundary
    {
        public float Up, Down, Left, Right;
        public Boundary(float up, float down, float left, float right)
        {
            Up = up; Down = down; Left = left; Right = right;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        R = transform.localScale.x/2;
        print(R);

        rb = GetComponent<Rigidbody2D>();

        playerBoundary = new Boundary(BoundaryHolder.GetChild(0).position.y,
                                      BoundaryHolder.GetChild(1).position.y,
                                      BoundaryHolder.GetChild(2).position.x,
                                      BoundaryHolder.GetChild(3).position.x);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            

            if (wasJustClicked)
            {
                wasJustClicked = false;
                double dist = Math.Sqrt((mousePos.x - transform.position.x) * (mousePos.x - transform.position.x) + (mousePos.y - transform.position.y) * (mousePos.y - transform.position.y));

                if (dist < R)
                {
                    mouseState = 1;
                }
                else
                {
                    mouseState = 0;

                }

            }
            if (mouseState == 1)
            {
                Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, playerBoundary.Left,
                                                        playerBoundary.Right),
                                                      Mathf.Clamp(mousePos.y, playerBoundary.Down,
                                                        playerBoundary.Up));
                rb.MovePosition(clampedMousePos);
            }
        }
        else
        {
            wasJustClicked = true;
        }
    
    }
}
